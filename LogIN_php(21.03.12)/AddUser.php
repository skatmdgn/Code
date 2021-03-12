<?php

$server = "localhost:3305";
$id = "root";
$pw = "12345678";
$db = "login_info";

$input_id = $_POST["ID"];
$input_pw = $_POST["Password"];

$conn = new mysqli($server, $id, $pw, $db);

if($conn->connect_error) {
	die ("connection failed: " . $conn->connect_error);
}

$sql = "SELECT * FROM user WHERE ID = '".$input_id."'";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	$update_sql = "UPDATE user SET Password = '".$input_pw."' WHERE ID = '".$input_id."'";
	$conn->query($update_sql);
}
else {
	$insert_sql = "INSERT INTO user(ID, Password) VALUES ('".$input_id."', '".$input_pw."')";
	$conn->query($insert_sql);
}

$conn->close();

?>