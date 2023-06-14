using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator.Examples
{
    public class Person
    {
        public string Name;
        public ChatRoom ChatRoom;

        private List<string> _chatLog = new();

        public Person(string name)
        {
            Name = name;
        }

        public void Say(string message)
        {
            ChatRoom.Broadcast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            ChatRoom.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            var s = $"{sender}: '{message}'";
            _chatLog.Add(s);
            Console.WriteLine($"[{Name}'s chat session]: {s}");
        }
    }

    public class ChatRoom
    {
        private List<Person> people = new();

        public void Join(Person p)
        {
            string joinMsg = $"{p.Name} joins the chat";
            Broadcast("room", joinMsg);
            p.ChatRoom = this;
            people.Add(p);
        }

        public void Broadcast(string source, string message)
        {
            foreach(var p in people)
            {
                if (p.Name == source)
                    continue;

                p.Receive(source, message);
            }
        }

        public void Message(string source, string destination, string mesage)
        {
            people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, mesage);
        }

        public static void Start(string[] args)
        {
            var room = new ChatRoom();

            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("hi");
            jane.Say("hey john");

            var simon = new Person("Simon");

            room.Join(simon);

            simon.Say("Hi everyone");

            jane.PrivateMessage("Simon", "Its private message");
        }
    }
}
