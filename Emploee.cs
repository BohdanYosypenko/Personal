using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Personal
{
    [Serializable]
    public class Emploee
    {
        protected internal event EmploeeStateHandler Added;
        protected internal event EmploeeStateHandler Deleted;
        protected internal event EmploeeStateHandler Edited;
        protected internal event EmploeeStateHandler ShowedEmploee;
        protected internal event EmploeeStateHandler Reported;


        static int counter = 0;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public int RoomNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Selary { get; set; }
        public string Notes { get; set; }

        public Emploee(string firstname, string secondname, DateTime dateOfBirth, string status, string department, int roomNumber, string phone, string email, int selary, string notes)
        {
            Id = ++counter;
            FirstName = firstname;
            SecondName = secondname;
            DateOfBirth = dateOfBirth;
            Status = status;
            Department = department;
            RoomNumber = roomNumber;
            Phone = phone;
            Email = email;
            Selary = selary;
            Notes = notes;
        }
        public Emploee()
        {

        }

        public override string ToString()
        {
            return String.Format(Id + "; " + FirstName + "; " + SecondName + "; " + DateOfBirth.ToShortDateString() + "; " + Status + "; " + Department + "; "
                + RoomNumber + "; " + Phone + "; " + Email + "; " + Selary + "; " + Notes + "; \n" );
        }

        private void CallEvent(EmploeeEventArgs e, EmploeeStateHandler handler)
        {
            if (e != null)
                handler!.Invoke(this, e);
        }

        protected void OnAdded(EmploeeEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected void OnDeleted(EmploeeEventArgs e)
        {
            CallEvent(e, Deleted);

        }
        protected void OnEdited(EmploeeEventArgs e)
        {
            CallEvent(e, Edited);

        }
        protected void OnShowedEmploee(EmploeeEventArgs e)
        {
            CallEvent(e, ShowedEmploee);
        }
        protected void OnReported(EmploeeEventArgs e)
        {
            CallEvent(e, Reported);
        }

        protected internal void Add()
        {
            OnAdded(new EmploeeEventArgs($"Додано нового працівника. Id працівника- {Id}"));

            
        }
        protected internal void Delete()
        {
            OnDeleted(new EmploeeEventArgs($"Працівника {Id} {FirstName} {SecondName} видалено з бази"));
        }
        protected internal void Show()
        {
            OnShowedEmploee(new EmploeeEventArgs($"{Id} - {FirstName} - {SecondName} "));
        }
        protected internal void Edit()
        {
            OnEdited(new EmploeeEventArgs($"Ви змінили дані працівника {Id} - {FirstName} - {SecondName}"));
        }


    }
}
