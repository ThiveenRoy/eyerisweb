$(document).ready(function () {

    var config = {
        apiKey: "AIzaSyCRQDgRN4O5ILUzNBs9-ruKP30ChRyjRHs",
        authDomain: "eeyes-68b9c.firebaseapp.com",
        databaseURL: "https://eeyes-68b9c.firebaseio.com/",
        projectId: "eeyes-68b9c",
        storageBucket: "eeyes-68b9c.appspot.com",
        messagingSenderId: "622818184974",
        //appId: "1:622818184974:web:159b82024c3ad0d9ded15f",
        //measurementId: "G-XM70Q185JQ"
    };

    firebase.initializeApp(config);
    console.log("hello");


    $(".register form").on("submit", function (event) {
        event.preventDefault();

        var email = $(".register .email").val();
        var password = $(".register .password").val();

        firebase.auth().createUserWithEmailAndPassword(email, password)
            .then(function (user) {
                console.log(user);
            })
            .catch(function (err) {
                console.log(err);
            });




    });

});