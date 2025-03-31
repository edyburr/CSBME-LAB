using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syringe
{
    
    public partial class Form1 : Form
    {
        private Panel syringe;
        private Panel emptyPart;
        private Panel medicinePart;
        private ComboBox medicineSelector;
        private Label unitLabel;
        private Button administerButton;
        private Button autoAdministerButton;
        private Button resetButton;
        private NumericUpDown maxDoseInput;
        private Dictionary<string, Medicine> medicines;
        private Dictionary<string, int> administeredAmounts = new Dictionary<string, int>();
        private Medicine currentMedicine;
        private int emptyAmount = 0;
        private int maxDose = 100;
        private bool autoAdministering = false;

        public Form1()
        {
            this.Text = "Injectomat Virtual";
            this.Size = new Size(515, 300);

            medicines = new Dictionary<string, Medicine>
            {
                { "Adrenalina", new Medicine { Name = "Adrenalina", Color = Color.Red } },
                { "Noradrenalina", new Medicine { Name = "Noradrenalina", Color = Color.Blue } },
                { "Novocaina", new Medicine { Name = "Novocaina", Color = Color.Green } },
                { "Aciclovir", new Medicine { Name = "Aciclovir", Color = Color.Purple } }
            };

            currentMedicine = medicines["Adrenalina"];

            foreach (var med in medicines.Keys)
            {
                administeredAmounts[med] = 0;
            }

            // SERINGA
            syringe = new Panel
            {
                Size = new Size(400, 50),
                Location = new Point(50, 30),
                BorderStyle = BorderStyle.FixedSingle
            };

            // NIVEL GOL
            emptyPart = new Panel
            {
                Size = new Size(emptyAmount * syringe.Width / 100, syringe.Height),
                Location = new Point(0, 0),
                BackColor = Color.Gray
            };

            // NIVEL MEDICAMENT
            medicinePart = new Panel
            {
                Size = new Size(currentMedicine.Amount * syringe.Width / 100, syringe.Height),
                Location = new Point(emptyPart.Width, 0),
                BackColor = currentMedicine.Color
            };

            syringe.Controls.Add(emptyPart);
            syringe.Controls.Add(medicinePart);
            this.Controls.Add(syringe);

            // MARCAJE
            for (int i = 0; i <= 100; i += 10)
            {
                Panel lineMarker = new Panel
                {
                    Size = new Size(1, 10),
                    Location = new Point(i * 4 + 50, syringe.Bottom),
                    BackColor = Color.Black
                };
                this.Controls.Add(lineMarker);

                if (i > 0)
                {
                    Label textMarker = new Label
                    {
                        Text = i.ToString(),
                        Location = new Point(i * syringe.Width / 100 + 20, syringe.Bottom + 12),
                        AutoSize = true,
                        Font = new Font("Helvetica", 8)
                    };
                    this.Controls.Add(textMarker);
                }
            }

            // SELECTOR MEDICAMENT
            medicineSelector = new ComboBox
            {
                Location = new Point(50, 120),
                Width = 100
            };
            medicineSelector.Items.AddRange(medicines.Keys.ToArray());
            medicineSelector.SelectedIndexChanged += MedicineSelector_SelectedIndexChanged;
            medicineSelector.SelectedItem = "Adrenalina";
            this.Controls.Add(medicineSelector);

            // SELECTOR AUTO-ADMINISTRARE
            maxDoseInput = new NumericUpDown
            {
                Location = new Point(175, 120),
                Width = 100,
                Minimum = 10,
                Maximum = 100,
                Value = 100
            };
            maxDoseInput.ValueChanged += (s, e) => maxDose = (int)maxDoseInput.Value;
            this.Controls.Add(maxDoseInput);

            // LABEL MAX
            unitLabel = new Label
            {
                Text = "Max",
                Location = new Point(285, 122),
                Font = new Font("Helvetica", 8)

            };
            this.Controls.Add(unitLabel);

            // BUTON ADMINISTRARE
            administerButton = new Button
            {
                Text = "Administrare",
                Location = new Point(50, 160),
                Width = 100
            };
            administerButton.Click += AdministerButton_Click;
            this.Controls.Add(administerButton);

            // BUTON AUTO-ADMINISTRARE
            autoAdministerButton = new Button
            {
                Text = "Auto-Administrează",
                Location = new Point(175, 160),
                Width = 150
            };
            autoAdministerButton.Click += AutoAdministerButton_Click;
            this.Controls.Add(autoAdministerButton);

            // BUTON RESETARE
            resetButton = new Button
            {
                Text = "Reset",
                Location = new Point(50, 200),
                Width = 100
            };
            resetButton.Click += ResetButton_Click;
            this.Controls.Add(resetButton);
        }

        private void MedicineSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = medicineSelector.SelectedItem.ToString();
            currentMedicine = medicines[selected];
            medicinePart.BackColor = currentMedicine.Color;
            emptyAmount = administeredAmounts[selected];

            UpdateSyringe();
        }

        private void AdministerButton_Click(object sender, EventArgs e)
        {
            AdministerMedicine(10, overrideMax: true);
        }

        private void AutoAdministerButton_Click(object sender, EventArgs e)
        {
            autoAdministering = !autoAdministering;
            autoAdministerButton.Text = autoAdministering ? "Stop" : "Auto-Administrează";
            if (autoAdministering)
            {
                new Thread(() =>
                {
                    while (autoAdministering && currentMedicine.Amount > 0 && emptyAmount < maxDose)
                    {
                        Invoke((Action)(() => AdministerMedicine(5, overrideMax: false)));
                        Thread.Sleep(1000);
                    }
                    autoAdministering = false;
                    Invoke((Action)(() => autoAdministerButton.Text = "Auto-Administrează"));
                }).Start();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            foreach (var med in medicines.Values)
                med.Amount = 100;

            foreach (var key in administeredAmounts.Keys.ToList())
            {
                administeredAmounts[key] = 0;
            }

            emptyAmount = 0;
            UpdateSyringe();
        }

        private void AdministerMedicine(int amount, bool overrideMax)
        {
            string medicineName = currentMedicine.Name;

            if (currentMedicine.Amount > 0 && (overrideMax || administeredAmounts[medicineName] < maxDose))
            {
                int dose = Math.Min(amount, currentMedicine.Amount);
                currentMedicine.Amount -= dose;

                administeredAmounts[medicineName] += dose;

                emptyAmount = administeredAmounts[medicineName];

                UpdateSyringe();
            }
        }

        private void UpdateSyringe()
        {
            emptyPart.Size = new Size(emptyAmount * syringe.Width / 100, syringe.Height);
            medicinePart.Size = new Size(currentMedicine.Amount * syringe.Width / 100, syringe.Height);
            medicinePart.Location = new Point(emptyPart.Width, 0);
        }
    }

    public class Medicine
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Amount { get; set; } = 100;
    }
}
