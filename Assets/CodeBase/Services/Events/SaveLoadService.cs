using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CodeBase.Services.Events
{
    public class SaveLoadEventsService: MonoBehaviour, ISaveLoadEventsService
    {
        public void Save(EventsContainer eventsContainer)
        {
            var destination = Application.persistentDataPath + "/events.dat";

            var file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);
 
            var bf = new BinaryFormatter();

            try
            {
                bf.Serialize(file, eventsContainer);
            }
            catch (Exception exception)
            {
                Debug.LogError("Events was not saved in file!");
                Debug.LogError(exception.Message);
            }
            
            file.Close();
        }
 
        public EventsContainer Load()
        {
            var destination = Application.persistentDataPath + "/events.dat";
            FileStream file;
 
            if(File.Exists(destination)) 
                file = File.OpenRead(destination);
            else
                return new EventsContainer();

            var bf = new BinaryFormatter();
            
            var data = (EventsContainer) bf.Deserialize(file);

            file.Close();

            return data;
        }
    }
}