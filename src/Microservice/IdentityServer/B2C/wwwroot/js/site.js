// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var _email;
$(document).ready(function () {
    $("#login-page").hide();
});

function validateEmail(email) {
    const pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return pattern.test(String(email).toLowerCase());
}

function findUser(email) {
    if (email == "") {
        document.getElementById('email-error').textContent = 'Enter an email';
        return;
    }
    else if (!validateEmail(email)) {
        document.getElementById('email-error').textContent = 'Enter a valid email';
        return;
    }
    document.getElementById('email-error').textContent = null;
    _email = email;
    const loginbox = document.getElementsByClassName('login-box')[0];
    const spinner = document.getElementById('spinner');

    spinner.removeAttribute('hidden');
    loginbox.style.opacity = 0.5;

    fetch('finduser?Email=' + email, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(user => {
            if (user != "") {
                $("#email-address").text(email);
                $("#username").text(user);

                $("#account-page").hide();
                $("#login-page").show();
            }
            else
                document.getElementById('email-error').textContent = 'User not found';
            spinner.setAttribute('hidden', true);
            loginbox.style.opacity = 1.0;
        })
        .catch(error => console.error('Unable to find user.', error));
}

function login(password) {
    if (password == "") {
        document.getElementById('password-error').textContent = "Enter a password";
        return;
    }
    document.getElementById('password-error').textContent = null;

    var formData = new FormData();
    formData.append('Email', _email);
    formData.append('Password', password);

    fetch('login', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(success => {
            if (success) {
                const params = new URLSearchParams(window.location.search);
                var returnurl = params.get('returnurl');

                window.location.href = returnurl;
            }
            else {
                document.getElementById('password-error').textContent = "Wrong password. Try again or click Forgot password to reset it.";
            }
        })
        .catch(error => console.error('Unable to find user.', error));
}

function togglePasswordVisibility() {
    const password = document.getElementById('password');
    const passwordIcon = document.getElementById('password-icon');
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    passwordIcon.classList.toggle('fa-eye', type === 'password');
    passwordIcon.classList.toggle('fa-eye-slash', type === 'text');
}
