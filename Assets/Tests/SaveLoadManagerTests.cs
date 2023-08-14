using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class SaveLoadManagerTests
    {
        private string myTestPlayer = "_TestPlayerPosition";
        private string myTestItem = "_TestItemCount";
       

        [SetUp]
        public void SetUp()
        {
            // Ensure PlayerPrefs is clean before each test
            PlayerPrefs.DeleteKey(myTestPlayer + "_x");
            PlayerPrefs.DeleteKey(myTestPlayer + "_y");
            PlayerPrefs.DeleteKey(myTestPlayer + "_z");
            PlayerPrefs.DeleteKey(myTestItem + "_TestItemCount");
        }
        
        [TearDown]
        public void TearDown()
        {
            PlayerPrefs.DeleteAll();
        }
        
        [Test]
        public void TestSaveAndLoadPositionAndItemCount()
        {
            // Mock object's init position
            Vector3 mockPosition = new Vector3(1.0f, 2.0f, 3.0f);
            // Mock item count
            int mockItemCount = 5;
            

            // Save object's position and item count
            SaveMockPosition(mockPosition);
            SaveMockItemCount(mockItemCount);

            // Load object's position and item count
            Vector3 loadedPosition = LoadMockPosition();
            int loadedItemCount = LoadMockItemCount();

            // Check if object'sposition and item count are same as saved ones
            Assert.AreEqual(mockPosition, loadedPosition);
            Assert.AreEqual(mockItemCount, loadedItemCount);
        }

        private void SaveMockPosition(Vector3 position)
        {
            PlayerPrefs.SetFloat(myTestPlayer + "_x", position.x);
            PlayerPrefs.SetFloat(myTestPlayer + "_y", position.y);
            PlayerPrefs.SetFloat(myTestPlayer + "_z", position.z);
            PlayerPrefs.Save();
        }
        
        private void SaveMockItemCount(int theItemCount)
        {

            PlayerPrefs.SetInt(myTestItem + "_TestItemCount", theItemCount);
            PlayerPrefs.Save();
        }


        private int LoadMockItemCount()
        {
            if (PlayerPrefs.HasKey(myTestItem + "_TestItemCount"))
            {
                return PlayerPrefs.GetInt(myTestItem + "_TestItemCount");
            }
            else
            {
                return 0; // Return zero if the item doesn't exist
            }
        }


        private Vector3 LoadMockPosition()
        {
            if (PlayerPrefs.HasKey(myTestPlayer + "_x") && PlayerPrefs.HasKey(myTestPlayer + "_y") && PlayerPrefs.HasKey(myTestPlayer + "_z"))
            {
                float x = PlayerPrefs.GetFloat(myTestPlayer + "_x");
                float y = PlayerPrefs.GetFloat(myTestPlayer + "_y");
                float z = PlayerPrefs.GetFloat(myTestPlayer + "_z");
                return new Vector3(x, y, z);
            }
            else
            {
                return Vector3.zero; // Return zero if object's vector if not found
            }
        }
        
    }
}



   
