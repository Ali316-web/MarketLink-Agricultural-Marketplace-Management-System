/* Light / dark theme toggle. Loaded in <head> so the saved theme applies before first paint. */
(function(){
"use strict";
var KEY="marketlink_admin_theme",root=document.documentElement;
function saved(){try{return localStorage.getItem(KEY)}catch(e){return null}}
function current(){
  var s=saved();
  if(s==="dark"||s==="light")return s;
  return window.matchMedia&&matchMedia("(prefers-color-scheme: dark)").matches?"dark":"light";
}
function labels(){
  var dark=root.getAttribute("data-theme")==="dark";
  var els=document.querySelectorAll('[data-a="theme"]');
  for(var i=0;i<els.length;i++){
    els[i].textContent=dark?"Light mode":"Dark mode";
    els[i].setAttribute("aria-pressed",dark?"true":"false");
  }
}
function apply(t){root.setAttribute("data-theme",t);labels()}
root.setAttribute("data-theme",current());
document.addEventListener("DOMContentLoaded",labels);
document.addEventListener("click",function(e){
  var b=e.target.closest&&e.target.closest('[data-a="theme"]');
  if(!b)return;
  var next=root.getAttribute("data-theme")==="dark"?"light":"dark";
  try{localStorage.setItem(KEY,next)}catch(err){}
  apply(next);
});
})();
