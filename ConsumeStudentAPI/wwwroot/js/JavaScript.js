$(document).ready(function () {
    $("#form").hide();
    loadRecord();
    $("#JavaScript").submit(function (event) {
        event.preventDefault();
    });
});

function loadRecord() {
    $.ajax(
        {
            type: 'GET',
            url: 'https://localhost:7080/api/Student',
            success: function (data) {
                $("#tbStudent").html("");
                for (var i = 0; i < data.length; i++) {
                    $('#tbStudent').append('<tr><td>' + data[i].id + '</td><td>' + data[i].name + '</td><td>' + data[i].address + '</td><td>' + data[i].phone + '</td></tr>');
                }
            },
            error: function (xhr) {
                alert(xhr);
            }
        });
}

function submitForm() {
    var formData = {
        Name: $("#txtName").val(),
        Address: $("#txtAddress").val(),
        Phone: $("#txtPhone").val()
    }
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7080/api/Student',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(formData),
        success: function (result) {
            alert('Successfully received Data ');
            console.log(result);
        },
        error: function (e) {
            alert('Failed to receive the Data');
            console.log(e.responseText);
        },
        beforeSend: function (xhr) {
            var token = getCookie("user");
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    });
}

function LoginBtn_Click() {
    var formData = {
        Name: $("#txtUser").val(),
        Phone: $("#txtPass").val()
    };
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7080/api/Jwt',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(formData),
        success: function (data) {
            setCookie("user", data.token, 1);
            document.cookie = 'jwtToken=${data.token}; path=/';
            window.location.href = "https://localhost:7234/Home/Privacy";
        },
        error: function (err) {
            alert('Failed to receive the Data');
            console.log('Failed '+err.responseText);
        }
    });
}

function GoBack() {
    window.location.href = "https://localhost:7234/Home/Privacy";
}

function setCookie(name, value, days) {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
}
function getCookie(name) {
    var nameEQ = encodeURIComponent(name) + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ')
            c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0)
            return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}
function eraseCookie(name) {
    createCookie(name, "", -1);
}


function genToken() {
    var formData = {
        Name: $("#txtUser").val(),
        Phone: $("#txtPass").val()
    };
    console.log(formData);
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7080/api/Jwt',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(formData),
        success: function (result) {
            alert(result.token);
            $("#token").html(result.token);
        },
        error: function (err) {
            alert('Invalid Cred.');
            console.log(err.responseText);
        }
    });
}

function UpdatePage() {
console.log("inside Update Student data function");
    $("table").hide();
    $("#CreateBtn").hide();
    $("#SubmitBtn").hide();
    $("#DeleteBtn").hide();
    $('#updatePg').hide();
    $('#deletePg').hide();
    $("#form").show();
    $('#title').html("Update Student Record");

}
function Update() {
    var formData = {
        Name: $("#txtName").val(),
        Address: $("#txtAddress").val(),
        Phone: $("#txtPhone").val()
    }
    var id = $("#txtId").val();
    $.ajax({
        type: 'PUT',
        url: 'https://localhost:7080/api/Student/'+id,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(formData),
        success: function (result) {
            alert('Successfully updated Data ');
            console.log(result);
        },
        error: function () {
            alert('Failed to Update the Data');
            console.log('Failed ');
        },
        beforeSend: function (xhr) {
            var token = getCookie("user");
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    });
}


function get() {
    $.ajax(
        {
            type: 'GET',
            url: 'https://localhost:7080/api/Student/3',
            success: function (data) {
                $('#output').html("Record fetched : Student Name : " + data);
                alert(data);
            },
            error: function (xhr) {
                $('#output').html(xhr);
                alert(xhr);
            }
        });
}

function DeletePage() {
console.log("inside Delete Student data function");
    $("table").hide();
    $("#CreateBtn").hide();
    $("#UpdateBtn").hide();
    $("#SubmitBtn").hide();
    $('#updatePg').hide();
    $('#deletePg').hide();
    $("#nameRow").hide();
    $("#addRow").hide();
    $("#phoneRow").hide();
    $("#form").show();
    $('#title').html("Delete Student Record");
}

function Delete() {    
    var id = $("#txtId").val();
    $.ajax(
        {
            type: 'DELETE',
            url: 'https://localhost:7080/api/Student/'+id,
            success: function (data) {
                alert("Record Deleted:" + data.name);
            },
            error: function (xhr) { 
                $('#output').html(xhr);
                alert(xhr);
            },
            beforeSend: function (xhr) {
                var token = getCookie("user");
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            }
        });
}

function CreateNew() {
    console.log("inside Adding new Student data function");
    $("table").hide();
    $("#CreateBtn").hide();
    $("#UpdateBtn").hide();
    $("#DeleteBtn").hide();
    $('#updatePg').hide();
    $('#deletePg').hide();
    $("#idRow").hide();
    $("#form").show();
    $('#title').html("Adding new Student record");
}
function Cancel() {
    document.getElementById('txtId').value = "";
    document.getElementById('txtName').value = "";
    document.getElementById('txtAddress').value = "";
    document.getElementById('txtPhone').value = "";
}
