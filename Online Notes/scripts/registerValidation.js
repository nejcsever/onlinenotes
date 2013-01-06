window.addEventListener("load", start, false);

function start() {
	document.getElementById("RegisterForm").addEventListener("submit", validateRegister, false);
}


function validateRegister(event) {
    var username = document.getElementById("UsernameInput");
	var email = document.getElementById("EmailInput");
	var password = document.getElementById("PasswordInput");
	var retypePassword = document.getElementById("RetypePasswordInput");
	
	var regEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	var regUsername = /^[a-zA-Z0-9._-]+$/;
	
	var valid = true;
	var usernameErrorMessage = "";
	var passwordErrorMessage = "";
	var emailErrorMessage = "";
	var retypePasswordErrorMessage = "";
	
	if (username.value == "") {
		valid = false;
		usernameErrorMessage = "Username field is required.";
	} else if (!regUsername.test(username.value)) {
		valid = false;
		usernameErrorMessage = "Not valid username (Allowed special char.: ._-).";
	}
	
	if (email.value == "") {
		valid = false;
		emailErrorMessage = "Email field is required.";
	} else if (!regEmail.test(email.value)) {
		valid = false;
		emailErrorMessage = "Enter a valid email (example@domain.com).";
	}
	
	if (password.value == "") {
		passwordErrorMessage  = "Password field is required.";
		valid =  false;
	} else if (password.value.length < 8) {
		passwordErrorMessage  = "Password must be at least 8 characters long.";
		valid =  false;
	} else if (password.value != retypePassword.value) {
		valid = false;
		retypePasswordErrorMessage = "Password and retyped password does not match.";
	}
	
	document.getElementById("username-error-message").innerHTML = usernameErrorMessage
	document.getElementById("email-error-message").innerHTML = emailErrorMessage
	document.getElementById("password-error-message").innerHTML = passwordErrorMessage
	document.getElementById("retype-password-error-message").innerHTML = retypePasswordErrorMessage

	if (!valid)
	    event.preventDefault();
}