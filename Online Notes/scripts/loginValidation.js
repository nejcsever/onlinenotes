window.addEventListener("load", start, false);

function start() {
	document.getElementById("LoginForm").addEventListener("submit", validateLogin, false);
}

function validateLogin(event) {
	var email = document.getElementById("EmailInput");
	var password = document.getElementById("PasswordInput");
	
	var regEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

	var valid = true;
	var passwordErrorMessage = "";
	var emailErrorMessage = "";
	
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
	}
	
	document.getElementById("email-error-message").innerHTML = emailErrorMessage
	document.getElementById("password-error-message").innerHTML = passwordErrorMessage
	
	if (!valid) {
	    event.preventDefault();
	}
}