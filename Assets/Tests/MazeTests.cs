using NUnit.Framework;
using System.Collections.Generic;
using Common.Scripts.Maze;
using UnityEngine;

/// <summary>
/// NUnit tests for the <c>Maze</c> script.
/// </summary>
public class MazeTests
{
    /// <summary>
    /// <c>Maze</c> script for use in the tests.
    /// </summary>
    private Maze myTestMaze;

    /// <summary>
    /// Called before every test to re-initialize the <c>myTestMaze</c> field
    /// and ensure valid state.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        myTestMaze = new GameObject().AddComponent<Maze>();
    }

    /// <summary>
    /// Test for the <c>myCurrentRoom</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetCurrentRoom()
    {
        Room testRoom = new GameObject().AddComponent<Room>();
        myTestMaze.MyCurrentRoom = testRoom;
        Assert.AreEqual(testRoom, myTestMaze.MyCurrentRoom);
    }

    /// <summary>
    /// Test for the <c>myCurrentDoor</c> mutator method.
    /// </summary>
    [Test]
    public void TestSetCurrentDoor()
    {
        Door testDoor = new GameObject().AddComponent<Door>();
        myTestMaze.MyCurrentDoor = testDoor;
        Assert.AreEqual(testDoor, myTestMaze.MyCurrentDoor);
    }

    /// <summary>
    /// Test for the <c>myLoseCondition</c> field.
    /// </summary>
    [Test]
    public void TestSetLoseCondition()
    {
        myTestMaze.MyLoseCondition = true;
        Assert.True(myTestMaze.MyLoseCondition);
    }

    /// <summary>
    /// Test for the <c>CheckLoseCondition()</c> method when there exists a path
    /// from the maze's win room to the player's current room. Initializes a mock
    /// 2x2 maze in which there is only one path to the player's current room. The
    /// path traverses each direction at least once to ensure that every case in
    /// the method's switch statement is tested.
    /// </summary>
    [Test]
    public void TestCheckLoseConditionFalse()
    {
        myTestMaze.MyRooms = new Room[3, 3];

        GameObject testDoor1 = new GameObject();
        Door doorScript1 = testDoor1.AddComponent<Door>();
        doorScript1.MyLockState = false;

        GameObject testDoor2 = new GameObject();
        Door doorScript2 = testDoor2.AddComponent<Door>();
        doorScript2.MyHasAttempted = false;

        GameObject testDoor3 = new GameObject();
        Door doorScript3 = testDoor3.AddComponent<Door>();
        doorScript3.MyLockState = false;

        GameObject testDoor4 = new GameObject();
        Door doorScript4 = testDoor4.AddComponent<Door>();
        doorScript4.MyHasAttempted = true;
        doorScript4.MyLockState = true;

        GameObject testDoor5 = new GameObject();
        Door doorScript5 = testDoor5.AddComponent<Door>();
        doorScript5.MyHasAttempted = false;

        GameObject testDoor6 = new GameObject();
        Door doorScript6 = testDoor6.AddComponent<Door>();
        doorScript6.MyHasAttempted = true;
        doorScript6.MyLockState = true;

        GameObject testDoor7 = new GameObject();
        Door doorScript7 = testDoor7.AddComponent<Door>();
        doorScript7.MyLockState = false;

        GameObject testDoor8 = new GameObject();
        Door doorScript8 = testDoor8.AddComponent<Door>();
        doorScript8.MyLockState = false;

        GameObject testDoor9 = new GameObject();
        Door doorScript9 = testDoor9.AddComponent<Door>();
        doorScript9.MyHasAttempted = false;

        GameObject testDoor10 = new GameObject();
        Door doorScript10 = testDoor10.AddComponent<Door>();
        doorScript10.MyHasAttempted = true;
        doorScript10.MyLockState = true;

        GameObject testDoor11 = new GameObject();
        Door doorScript11 = testDoor11.AddComponent<Door>();
        doorScript11.MyHasAttempted = true;
        doorScript11.MyLockState = true;

        GameObject testDoor12 = new GameObject();
        Door doorScript12 = testDoor12.AddComponent<Door>();
        doorScript12.MyLockState = false;

        Room testRoom1 = new GameObject().AddComponent<Room>();
        testRoom1.MyRow = 1;
        testRoom1.MyCol = 1;
        testRoom1.MyDoors = new List<GameObject>
        {
            new GameObject("no-door"),
            testDoor1,
            testDoor3,
            new GameObject("no-door")
        };

        Room testRoom2 = new GameObject().AddComponent<Room>();
        testRoom2.MyRow = 1;
        testRoom2.MyCol = 2;
        testRoom2.MyDoors = new List<GameObject>
        {
            new GameObject("no-door"),
            testDoor2,
            testDoor4,
            testDoor1
        };

        Room testRoom3 = new GameObject().AddComponent<Room>();
        testRoom3.MyRow = 1;
        testRoom3.MyCol = 3;
        testRoom3.MyDoors = new List<GameObject>
        {
            new GameObject("no-door"),
            new GameObject("no-door"),
            testDoor5,
            testDoor2
        };

        Room testRoom4 = new GameObject().AddComponent<Room>();
        testRoom4.MyRow = 2;
        testRoom4.MyCol = 1;
        testRoom4.MyDoors = new List<GameObject>
        {
            testDoor3,
            testDoor6,
            testDoor8,
            new GameObject("no-door")
        };

        Room testRoom5 = new GameObject().AddComponent<Room>();
        testRoom5.MyRow = 2;
        testRoom5.MyCol = 2;
        testRoom5.MyDoors = new List<GameObject>
        {
            testDoor4,
            testDoor7,
            testDoor9,
            testDoor6
        };

        Room testRoom6 = new GameObject().AddComponent<Room>();
        testRoom6.MyRow = 2;
        testRoom6.MyCol = 3;
        testRoom6.MyDoors = new List<GameObject>
        {
            testDoor5,
            new GameObject("no-door"),
            testDoor10,
            testDoor7
        };

        Room testRoom7 = new GameObject().AddComponent<Room>();
        testRoom7.MyRow = 3;
        testRoom7.MyCol = 1;
        testRoom7.MyDoors = new List<GameObject>
        {
            testDoor8,
            testDoor11,
            new GameObject("no-door"),
            new GameObject("no-door")
        };

        Room testRoom8 = new GameObject().AddComponent<Room>();
        testRoom8.MyRow = 3;
        testRoom8.MyCol = 2;
        testRoom8.MyDoors = new List<GameObject>
        {
            testDoor9,
            testDoor12,
            new GameObject("no-door"),
            testDoor11
        };

        Room testRoom9 = new GameObject().AddComponent<Room>();
        testRoom9.MyRow = 3;
        testRoom9.MyCol = 3;
        testRoom9.MyDoors = new List<GameObject>
        {
            testDoor10,
            new GameObject("no-door"),
            new GameObject("no-door"),
            testDoor12
        };

        myTestMaze.MyRooms[0, 0] = testRoom1;
        myTestMaze.MyRooms[0, 1] = testRoom2;
        myTestMaze.MyRooms[0, 2] = testRoom3;
        myTestMaze.MyRooms[1, 0] = testRoom4;
        myTestMaze.MyRooms[1, 1] = testRoom5;
        myTestMaze.MyRooms[1, 2] = testRoom6;
        myTestMaze.MyRooms[2, 0] = testRoom7;
        myTestMaze.MyRooms[2, 1] = testRoom8;
        myTestMaze.MyRooms[2, 2] = testRoom9;

        myTestMaze.MyCurrentRoom = myTestMaze.MyRooms[2, 0];

        Assert.False(myTestMaze.CheckLoseCondition(3, 3, new bool[3, 3]));
    }

    /// <summary>
    /// Test for the <c>CheckLoseCondition()</c> method when no path exists
    /// from the maze's win room to the player's current room. The mock maze
    /// has the same state as the previous method, with the exception being
    /// that the player's current room has only locked doors.
    /// </summary>
    [Test]
    public void TestCheckLoseConditionTrue()
    {
        myTestMaze.MyRooms = new Room[3, 3];

        GameObject testDoor1 = new GameObject();
        Door doorScript1 = testDoor1.AddComponent<Door>();
        doorScript1.MyLockState = false;

        GameObject testDoor2 = new GameObject();
        Door doorScript2 = testDoor2.AddComponent<Door>();
        doorScript2.MyHasAttempted = false;

        GameObject testDoor3 = new GameObject();
        Door doorScript3 = testDoor3.AddComponent<Door>();
        doorScript3.MyLockState = false;

        GameObject testDoor4 = new GameObject();
        Door doorScript4 = testDoor4.AddComponent<Door>();
        doorScript4.MyHasAttempted = true;
        doorScript4.MyLockState = true;

        GameObject testDoor5 = new GameObject();
        Door doorScript5 = testDoor5.AddComponent<Door>();
        doorScript5.MyHasAttempted = false;

        GameObject testDoor6 = new GameObject();
        Door doorScript6 = testDoor6.AddComponent<Door>();
        doorScript6.MyHasAttempted = true;
        doorScript6.MyLockState = true;

        GameObject testDoor7 = new GameObject();
        Door doorScript7 = testDoor7.AddComponent<Door>();
        doorScript7.MyLockState = false;

        GameObject testDoor8 = new GameObject();
        Door doorScript8 = testDoor8.AddComponent<Door>();
        doorScript8.MyHasAttempted = true;
        doorScript8.MyLockState = true;

        GameObject testDoor9 = new GameObject();
        Door doorScript9 = testDoor9.AddComponent<Door>();
        doorScript9.MyHasAttempted = false;

        GameObject testDoor10 = new GameObject();
        Door doorScript10 = testDoor10.AddComponent<Door>();
        doorScript10.MyHasAttempted = true;
        doorScript10.MyLockState = true;

        GameObject testDoor11 = new GameObject();
        Door doorScript11 = testDoor11.AddComponent<Door>();
        doorScript11.MyHasAttempted = true;
        doorScript11.MyLockState = true;

        GameObject testDoor12 = new GameObject();
        Door doorScript12 = testDoor12.AddComponent<Door>();
        doorScript12.MyLockState = false;

        Room testRoom1 = new GameObject().AddComponent<Room>();
        testRoom1.MyRow = 1;
        testRoom1.MyCol = 1;
        testRoom1.MyDoors = new List<GameObject>
        {
            new GameObject("no-door"),
            testDoor1,
            testDoor3,
            new GameObject("no-door")
        };

        Room testRoom2 = new GameObject().AddComponent<Room>();
        testRoom2.MyRow = 1;
        testRoom2.MyCol = 2;
        testRoom2.MyDoors = new List<GameObject>
        {
            new GameObject("no-door"),
            testDoor2,
            testDoor4,
            testDoor1
        };

        Room testRoom3 = new GameObject().AddComponent<Room>();
        testRoom3.MyRow = 1;
        testRoom3.MyCol = 3;
        testRoom3.MyDoors = new List<GameObject>
        {
            new GameObject("no-door"),
            new GameObject("no-door"),
            testDoor5,
            testDoor2
        };

        Room testRoom4 = new GameObject().AddComponent<Room>();
        testRoom4.MyRow = 2;
        testRoom4.MyCol = 1;
        testRoom4.MyDoors = new List<GameObject>
        {
            testDoor3,
            testDoor6,
            testDoor8,
            new GameObject("no-door")
        };

        Room testRoom5 = new GameObject().AddComponent<Room>();
        testRoom5.MyRow = 2;
        testRoom5.MyCol = 2;
        testRoom5.MyDoors = new List<GameObject>
        {
            testDoor4,
            testDoor7,
            testDoor9,
            testDoor6
        };

        Room testRoom6 = new GameObject().AddComponent<Room>();
        testRoom6.MyRow = 2;
        testRoom6.MyCol = 3;
        testRoom6.MyDoors = new List<GameObject>
        {
            testDoor5,
            new GameObject("no-door"),
            testDoor10,
            testDoor7
        };

        Room testRoom7 = new GameObject().AddComponent<Room>();
        testRoom7.MyRow = 3;
        testRoom7.MyCol = 1;
        testRoom7.MyDoors = new List<GameObject>
        {
            testDoor8,
            testDoor11,
            new GameObject("no-door"),
            new GameObject("no-door")
        };

        Room testRoom8 = new GameObject().AddComponent<Room>();
        testRoom8.MyRow = 3;
        testRoom8.MyCol = 2;
        testRoom8.MyDoors = new List<GameObject>
        {
            testDoor9,
            testDoor12,
            new GameObject("no-door"),
            testDoor11
        };

        Room testRoom9 = new GameObject().AddComponent<Room>();
        testRoom9.MyRow = 3;
        testRoom9.MyCol = 3;
        testRoom9.MyDoors = new List<GameObject>
        {
            testDoor10,
            new GameObject("no-door"),
            new GameObject("no-door"),
            testDoor12
        };

        myTestMaze.MyRooms[0, 0] = testRoom1;
        myTestMaze.MyRooms[0, 1] = testRoom2;
        myTestMaze.MyRooms[0, 2] = testRoom3;
        myTestMaze.MyRooms[1, 0] = testRoom4;
        myTestMaze.MyRooms[1, 1] = testRoom5;
        myTestMaze.MyRooms[1, 2] = testRoom6;
        myTestMaze.MyRooms[2, 0] = testRoom7;
        myTestMaze.MyRooms[2, 1] = testRoom8;
        myTestMaze.MyRooms[2, 2] = testRoom9;

        myTestMaze.MyCurrentRoom = myTestMaze.MyRooms[2, 0];

        Assert.True(myTestMaze.CheckLoseCondition(3, 3, new bool[3, 3]));
    }
}