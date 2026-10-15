/* Login page only. */
(function(){
"use strict";
var form=document.getElementById("login-form"),err=document.getElementById("l-err");
form.addEventListener("submit",function(e){
  e.preventDefault();
  var em=document.getElementById("l-email").value.trim().toLowerCase(),pw=document.getElementById("l-pass").value;
  if(!em||!pw){err.textContent="Enter your email and password.";return}
  if(em!=="admin@marketlink.com"||pw!=="Admin@123"){err.textContent="Email or password is incorrect. Only admin accounts can sign in here.";return}
  location.href="dashboard.html";
});
})();
