<?php

$server = "localhost:3305";
$id = "root";
$pw = "12345678";
$db = "login_info";

$input_id = $_POST["ID"];

$conn = new mysqli($server, $id, $pw, $db);

if($conn->connect_error){
	die("connection failed: ".$conn->connect_error);
}

$sql = "SELECT * FROM user WHERE ID = '".$input_id."'";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	echo True;
}

$conn->close();

?>
