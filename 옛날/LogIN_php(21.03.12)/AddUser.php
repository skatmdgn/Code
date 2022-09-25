<?php

$server = "localhost:3305";
$id = "root";
$pw = "12345678";
$db = "login_info";

$input_id = $_POST["ID"];
$input_pw = $_POST["Password"];

$conn = new mysqli($server, $id, $pw, $db);

$sql = "INSERT INTO user(ID, Password) VALUES ('".$input_id."', '".$input_pw."')";
$conn->query($sql);

$conn->close();

?>
