using DamuMed.Data;
using DamuMed.Models;
using DamuMed.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.UI
{
    public class Menu
    {

        private PatientDataAccess _patientDataAccess;
        private DoctorDataAccess _doctorDataAccess;
        private ScheduleDataAccess _scheduleDataAccess;
        private Patient currentPatient;


        public Menu()
        {
            _patientDataAccess = new PatientDataAccess();
            _doctorDataAccess = new DoctorDataAccess();
            _scheduleDataAccess = new ScheduleDataAccess();
        }

        public void MainMenu()
        {

            bool isEnd = false;
            while (!isEnd)
            {
                Console.Clear();
                ShowMenu("----------Приложение DamuMed----------", "Войти в систему", "Зарегистрироваться в системе", "Выйти");

                int.TryParse(Console.ReadLine(), out int choose);

                switch (choose)
                {
                    case 1:
                        AuthorizationMenu();
                        break;

                    case 2:
                        RegistrationMenu();
                        break;
                    case 3:
                        isEnd = true;
                        break;

                    default:
                        Console.WriteLine("Вы ввели неправильный Индекс!");
                        Console.ReadLine();
                        break;
                }

            }

        }


        public void RegistrationMenu()
        {
            Patient newPatient = new Patient();
            var isEnd = false;
            while (!isEnd)
            {
                Console.Clear();
                Console.WriteLine("----------Меню регистрации----------");
                var patient = MenuGetPatientInfo();

                if (patient["name"].Length == 0 || patient["IIN"].Length == 0)
                {
                    Console.WriteLine("Вы ввели неправильно количество символов!");
                    Console.ReadLine();
                    isEnd = true;
                }

                else
                {
                    newPatient.Name = patient["name"];
                    newPatient.IIN = patient["IIN"];

                    var patients = _patientDataAccess.SelectBy(newPatient.Name, newPatient.IIN);
                    if (patients.Count == 0)
                    {
                        _patientDataAccess.Insert(newPatient);
                        isEnd = true;
                    }
                    else
                    {
                        Console.WriteLine("Пользователь с такими данными уже существует!");
                        Console.ReadLine();
                        isEnd = true;
                    }


                }

            }


        }

        public void AuthorizationMenu()
        {

            var isEnd = false;
            while (!isEnd)
            {
                Console.Clear();
                Console.WriteLine("----------Меню авторизации----------");
                var patient = MenuGetPatientInfo();

                if (patient["name"].Length == 0 || patient["IIN"].Length == 0)
                {
                    Console.WriteLine("Вы ввели неправильно количество символов!");
                    Console.ReadLine();
                    isEnd = true;
                }
                else
                {
                    var patients = _patientDataAccess.SelectBy(patient["name"], patient["IIN"]);
                    if (patients.Count != 0)
                    {
                        currentPatient = new Patient()
                        {
                            Name = patient["name"],
                            IIN = patient["IIN"]
                        };
                        MenuRecording();
                        isEnd = true;
                    }
                    else
                    {
                        Console.WriteLine("Пользователя с такими данными не существует!");
                        Console.ReadLine();
                        isEnd = true;
                    }


                }

            }

        }


        public void MenuRecording()
        {
            var isEnd = false;
            while (!isEnd)
            {

                Console.Clear();
                Console.WriteLine("----------Меню записи к врачу----------");


                List<Doctor> doctors = _doctorDataAccess.SelectBy();

                if (doctors.Count == 0)
                {
                    Console.WriteLine("Докторов нет!");
                    Console.ReadLine();
                    isEnd = true;
                    break;
                }

                var counter = 1;
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"\nномер №{counter++}");
                    Console.WriteLine("-----------------------");
                    Console.WriteLine(doctor.ToString());
                }

                Console.WriteLine("Выберите врача: ");

                int.TryParse(Console.ReadLine(), out int choose);

                if (choose <= 0 && choose > doctors.Count)
                {
                    Console.WriteLine("Вы ввели неправильный индекс!");
                    Console.ReadLine();
                }


                Console.WriteLine("Выберите Год: ");
                int.TryParse(Console.ReadLine(), out int year);

                Console.WriteLine("Выберите Месяц:");
                int.TryParse(Console.ReadLine(), out int month);

                Console.WriteLine("Выберите день:");
                int.TryParse(Console.ReadLine(), out int day);

                Console.WriteLine("Выберите час:");
                int.TryParse(Console.ReadLine(), out int hour);

                DateTime date = new DateTime(year,month,day);
                date.AddHours(hour);

                var schedules = _scheduleDataAccess.SelectBy(doctors[choose-1].Id,date);
                if (schedules.Count == 0 || schedules.Count ==1)
                {
                    var schedule = new Schedule()
                    {
                        DoctorId = doctors[choose - 1].Id,
                        PatientId = currentPatient.Id,
                        DateTime = date                  
                    };
                    _scheduleDataAccess.Insert(schedule);
                    Console.WriteLine("Вы заппсаны!");
                }

            }
        }



        public Dictionary<string, string> MenuGetPatientInfo()
        {
            string name = string.Empty, IIN = string.Empty;

            Dictionary<string, string> patient = new Dictionary<string, string>();

            Console.Write("Введите Ваше ФИО: ");
            name = Console.ReadLine();

            Console.Write("Введите Ваш ИИН (12 символов): ");
            IIN = Console.ReadLine();

            if (CorrectService.CorrectIIN(IIN))
            {
                patient.Add("name", name);
                patient.Add("IIN", IIN);
            }

            return patient;
        }


        private void ShowMenu(string title, params string[] actions)
        {
            Console.WriteLine(title);
            for (int i = 0; i < actions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {actions[i]}");
            }
        }

    }
}
