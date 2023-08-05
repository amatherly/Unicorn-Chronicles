<?php
// Replace these with your database credentials
$servername = "Carolines-MacBook-Pro.local";
$username = "root";
$password = "[null]";
$dbname = "Trivia";

// Create a connection to the database
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Query to retrieve questions and answers from the database
$sql = "SELECT Question, RightAnswer FROM trivia_table";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Create an array to store the questions and answers
    $questions = array();

    // Loop through each row in the result and add it to the array
    while ($row = $result->fetch_assoc()) {
        $questions[] = $row;
    }

    // Convert the array to JSON and print it
    echo json_encode($questions);
} else {
    echo "No questions found.";
}

$conn->close();
?>