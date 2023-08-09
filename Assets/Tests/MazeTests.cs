// using Common.Scripts.Maze;
// using NUnit.Framework;
// using Singleton;
// using UnityEngine;
//
//
// public class MazeTests
// {
//     [Test]
//     public void SetDoorLockState_LocksDoorAndChecksLoseCondition()
//     {
//         Maze maze = new GameObject().AddComponent<Maze>();
//         Room room = new GameObject().AddComponent<Room>();
//         DoorController door = new GameObject().AddComponent<DoorController>();
//
//         maze.MyCurrentRoom = room;
//         maze.MyCurrentDoor = door;
//
//         maze.SetDoorLockState(true);
//
//         Assert.IsTrue(door.MyLockState);
//         Assert.IsTrue(maze.MyLoseCondition);
//     }
//
//     [Test]
//     public void SetDoorLockState_UnlocksDoorAndDoesNotSetLoseCondition()
//     {
//         Maze maze = new GameObject().AddComponent<Maze>();
//         Room room = new GameObject().AddComponent<Room>();
//         DoorController door = new GameObject().AddComponent<DoorController>();
//
//         maze.MyCurrentRoom = room;
//         maze.MyCurrentDoor = door;
//
//         maze.SetDoorLockState(false);
//
//         Assert.IsFalse(door.MyLockState);
//         Assert.IsFalse(maze.MyLoseCondition);
//     }
//
//     [Test]
//     public void CheckLoseCondition_PlayerInCurrentRoom_ReturnsFalse()
//     {
//         Maze maze = new GameObject().AddComponent<Maze>();
//         Room room = new GameObject().AddComponent<Room>();
//         maze.MyCurrentRoom = room;
//
//         bool result = maze.CheckLoseCondition(room.MyRow, room.MyCol, new bool[4, 4]);
//
//         Assert.IsFalse(result);
//     }
//     
//     [Test]
//     public void PopulateMaze_PopulatesAllRooms()
//     {
//         GameObject mazeObject = new GameObject("Maze");
//         Maze maze = mazeObject.AddComponent<Maze>();
//
//
//         GameObject[] mockRoomObjects = new GameObject[16];
//
//         for (int i = 0; i < 16; i++)
//         {
//             mockRoomObjects[i] = new GameObject($"Mock Room {i + 1}");
//             mockRoomObjects[i].AddComponent<Room>();
//         }
//         
//         maze.MyRooms = new Room[4, 4];
//         for (int i = 0; i < 4; i++)
//         {
//             for (int j = 0; j < 4; j++)
//             {
//                 maze.MyRooms[i, j] = mockRoomObjects[i * 4 + j].GetComponent<Room>();
//             }
//         }
//
//         maze.PopulateMaze();
//
//         for (int i = 0; i < 4; i++)
//         {
//             for (int j = 0; j < 4; j++)
//             {
//                 Assert.NotNull(maze.MyRooms[i, j], $"Room at {i}-{j} should not be null");
//             }
//         }
//     }
//     
// }