using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class SaveLoadManagerTests
    {
        private string PLAYER_POS = "_TestPlayerPosition";
        private string ITEM = "_TestItemCount";
       

        [SetUp]
        public void SetUp()
        {
            // Ensure PlayerPrefs is clean before each test
            PlayerPrefs.DeleteKey(PLAYER_POS + "_x");
            PlayerPrefs.DeleteKey(PLAYER_POS + "_y");
            PlayerPrefs.DeleteKey(PLAYER_POS + "_z");
            PlayerPrefs.DeleteKey(PLAYER_POS + "_TestItemCount");
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
            PlayerPrefs.SetFloat(PLAYER_POS + "_x", position.x);
            PlayerPrefs.SetFloat(PLAYER_POS + "_y", position.y);
            PlayerPrefs.SetFloat(PLAYER_POS + "_z", position.z);
            PlayerPrefs.Save();
        }
        
        private void SaveMockItemCount(int theItemCount)
        {

            PlayerPrefs.SetInt(ITEM + "_TestItemCount", theItemCount);
            PlayerPrefs.Save();
        }
        
        private int LoadMockItemCount()
        {
            if (PlayerPrefs.HasKey(ITEM + "_TestItemCount"))
            {
                return PlayerPrefs.GetInt(ITEM + "_TestItemCount");
            }
            else
            {
                return 0; // Return zero if the item doesn't exist
            }
        }
        
        private Vector3 LoadMockPosition()
        {
            if (PlayerPrefs.HasKey(PLAYER_POS + "_x") && PlayerPrefs.HasKey(PLAYER_POS + "_y") && PlayerPrefs.HasKey(PLAYER_POS + "_z"))
            {
                float x = PlayerPrefs.GetFloat(PLAYER_POS + "_x");
                float y = PlayerPrefs.GetFloat(PLAYER_POS + "_y");
                float z = PlayerPrefs.GetFloat(PLAYER_POS + "_z");
                return new Vector3(x, y, z);
            }
            else
            {
                return Vector3.zero; // Return zero if object's vector if not found
            }
        }
        
    }
}



   
