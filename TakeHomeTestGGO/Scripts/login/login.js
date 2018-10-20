$(document).ready(function () {

    $("#guardar").click(function (e) {
        e.preventDefault();

        var url = '/api/user/Login';

        var NewResultado = {};
        var person = new Object();
        console.log("Usuario " + $("#username").val() + " PAS " + $("#password").val());
        person.userId = $("#username").val();
        person.name = "prueba";
        person.email = "dos";
        person.password = $("#password").val();


        $.ajax({
            url: url,
            type: 'POST',
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "userId": $("#username").val(),"password": $("#password").val()}),
            complete: function (jqXhr, textStatus) {
                var result = jQuery.parseJSON(jqXhr.responseText);
                console.log("textStatus ", textStatus);

                if (textStatus = 'success') {
                    if (result == true) {
                        window.location.href = '/Individual/Person'
                    }
                } else if (textStatus = 'success'){
                    console.log("Error");
                }
                
                console.log("Data", jqXhr);
            }

        });
    });
});