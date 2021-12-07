using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netCard_Client_Console_Fixed1
{
    public class Client
    {
        string name, lastName, responsibility, sign, number;

        //Constructor
        public Client(string name, string lastName,string responsibility, string sign, string number)
        {
            this.name = name;
            this.lastName = lastName;
            this.responsibility = responsibility;
            this.sign = sign;
            this.number = number;
        }

        public Client() { }

        public string getName()
        {
            return name;
        }

        public void setName(string newName)
        {
            name = newName;
        }

        public string getLastName()
        {
            return lastName;
        }

        public void setLastName(string newLastName)
        {
            lastName = newLastName;
        }

        public string getResponsibilities()
        {
            return responsibility;
        }

        public void setResponsibilities(string newResponsibility)
        {
            responsibility = newResponsibility;
        }

        public string getNumber()
        {
            return number;
        }

        public void setNumber(string newNumber)
        {
            number = newNumber;
        }

        public string getSign()
        {
            return sign;
        }

        public void setSign(string newSign)
        {
            sign = newSign;
        }

        public String toStringGetUserInfo()
        {
            return ("Name: " + name 
                + "\nSurname: " + lastName
                + "\nResponsibility: " + responsibility 
                + "\nCard number: " + number 
                + "\nSignature: " + sign + "\n");
        }
    }

}
