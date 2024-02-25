using EventSpeakers.Data;
using EventSpeakers.Models;

string opt;

SpeakerDao sDao = new SpeakerDao();
EventDao evntDao = new EventDao();

do
{
    Console.WriteLine("\n1. Insert speaker");
    Console.WriteLine("2. Update speaker");
    Console.WriteLine("3. Get speaker by Id");
    Console.WriteLine("4. Get all speakers");
    Console.WriteLine("5. Delete speaker by Id");
    Console.WriteLine("6. Get event by Id");
    Console.WriteLine("7. Get all events");
    Console.WriteLine("8. Create event");
    Console.WriteLine("0. Exit");
    opt = Console.ReadLine();

    string fullname, position, company, imageurl,sIDStr, eIDStr,startdateStr,name,description,adress, startsatStr,endsatStr;
    DateTime startdate;
    int sID,eID;
    switch (opt)
    {
        case "1":
            do
            {
                Console.Write("\nEnter fullname - ");
                fullname = Console.ReadLine();
            } while (string.IsNullOrEmpty(fullname));
            do
            {
                Console.Write(" Enter position - ");
                position = Console.ReadLine();
            } while (string.IsNullOrEmpty(position));
            do
            {
                Console.Write("  Enter company - ");
                company = Console.ReadLine();
            } while (string.IsNullOrEmpty(company));
            do
            {
                Console.Write("   Enter imageurl - ");
                imageurl = Console.ReadLine();
            } while (string.IsNullOrEmpty(imageurl));

            Speaker spkr = new Speaker(fullname, position, company, imageurl);
            var r = sDao.InsertSpeaker(spkr);
            if(r == 0) Console.WriteLine("Couldn't insert ! Try again later");
            else Console.WriteLine("Speaker added !");

            break;

        case "2":
            do
            {
                Console.Write("Insert speaker id - ");
                sIDStr = Console.ReadLine();
            } while (!int.TryParse(sIDStr,out sID) || sID<0);
            do
            {
                Console.Write("\nEnter fullname - ");
                fullname = Console.ReadLine();
            } while (string.IsNullOrEmpty(fullname));
            do
            {
                Console.Write(" Enter position - ");
                position = Console.ReadLine();
            } while (string.IsNullOrEmpty(position));
            do
            {
                Console.Write("  Enter company - ");
                company = Console.ReadLine();
            } while (string.IsNullOrEmpty(company));
            

            Speaker updatedSpeaker = new Speaker
            {
                Id = sID,
                Fullname = fullname,
                Position = position,
                Company = company
            };
            var updt = sDao.InsertSpeaker(updatedSpeaker);
            if (updt == 0) Console.WriteLine("Couldn't update ! Try again later");
            else Console.WriteLine("Speaker updated !");
            break;

        case "3":
            do
            {
                Console.Write("Insert speaker id - ");
                sIDStr = Console.ReadLine();
            } while (!int.TryParse(sIDStr, out sID) || sID < 0);

            Console.WriteLine(sDao.GetSpeakerById(sID));
            break;

        case "4":
            Console.WriteLine("\n All speakers \n =====================");
            foreach (var item in sDao.GetSpeakers())
            {
                Console.WriteLine(item);
            }
            break;

        case "5":
            do
            {
                Console.Write("Insert speaker id - ");
                sIDStr = Console.ReadLine();
            } while (!int.TryParse(sIDStr, out sID) || sID < 0);

            r = sDao.DeleteSpeaker(sID);
            if (r == 0) Console.WriteLine("Couldn't delete ! Try again later");
            else Console.WriteLine("Speaker deleted !");
            break;

        case "6":
            do
            {
                Console.Write("\nInsert event id - ");
                eIDStr = Console.ReadLine();
            } while (!int.TryParse(eIDStr, out eID) || eID < 0);

            var evnnt = evntDao.GetEventById(eID);
            if(evnnt == null ) Console.WriteLine("Not found !");
            Console.WriteLine(evnnt);

            break;

        case "7":
            Console.WriteLine("\nAll events \n ========================");
            if(evntDao.GetEvents() == null) Console.WriteLine("No events !");
            foreach (var item in evntDao.GetEvents())
            {
                Console.WriteLine(item);
            }
            break;

        case "8":
            do
            {
                Console.Write("\nEnter event's name - ");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));
            do
            {
                Console.Write(" Enter event's description - ");
                description = Console.ReadLine();
            } while (string.IsNullOrEmpty(description));
            do
            {
                Console.Write("  Enter event's adress - ");
                adress = Console.ReadLine();
            } while (string.IsNullOrEmpty(adress));
            do
            {
                Console.Write("   Enter event's date - ");
                startdateStr = Console.ReadLine();
            } while (!DateTime.TryParse(startdateStr,out startdate));
            do
            {
                Console.Write("   Event starts at - ");
                startsatStr = Console.ReadLine();
            } while (string.IsNullOrEmpty(startsatStr));
            do
            {
                Console.Write("   Event ends at - ");
                endsatStr = Console.ReadLine();
            } while (string.IsNullOrEmpty(endsatStr));

            do
            {
                Console.Write("Enter speaker id for event  - ");
                sIDStr = Console.ReadLine();
            } while (!int.TryParse(sIDStr, out sID));

            Eventt eveent = new Eventt(name,description,adress,startdate,startsatStr,endsatStr);
            var y = evntDao.InsertEvent(eveent);

            if (y != 0) 
            {
                evntDao.AddSpeaker(y, sID);
                Console.WriteLine("Event added!");
            }
            else
            {
                Console.WriteLine("Couldn't insert event! Try again later!");
            }

            break;

        case "0":
            Console.WriteLine("Goodbye !");
            break;

        default:
            Console.WriteLine("Insert correct operator !");
            break;
    }
} while (opt != "0");