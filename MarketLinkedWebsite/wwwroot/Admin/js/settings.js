/* System configuration section */
(function(){
"use strict";
const {db,st,esc,money,badge,empty,marketName,farmerName,mapLink}=ML;
ML.mount(()=>`
  <div class="page-head"><div><h1>System configuration</h1><p>Master data and platform-wide announcements.</p></div></div>
  <div class="grid2" style="align-items:start">
    <div class="panel"><h2>Product categories</h2>
      <div class="chips">${db.categories.map((c,i)=>`<span class="chip">${esc(c)}<button data-a="cat-edit" data-id="${i}" aria-label="Rename ${esc(c)}">Edit</button><button class="x" data-a="cat-del" data-id="${i}" aria-label="Delete ${esc(c)}">Delete</button></span>`).join("")||empty("No categories.")}</div>
      <form class="add-row" id="cat-form"><input type="text" id="cat-name" placeholder="New category name" aria-label="New category name"><button class="btn primary" type="submit">Add category</button></form>
      <div class="err" id="cat-err" role="alert" style="margin:8px 0 0"></div>
    </div>
    <div class="panel"><h2>Publish announcement</h2>
      <form id="ann-form">
        <div class="field"><label for="a-title">Title</label><input id="a-title" type="text" maxlength="80"></div>
        <div class="field"><label for="a-body">Message</label><textarea id="a-body" maxlength="300"></textarea></div>
        <div class="field"><label for="a-aud">Audience</label><select id="a-aud"><option>All users</option><option>Farmers</option><option>Customers</option></select></div>
        <div class="err" id="a-err" role="alert"></div>
        <button class="btn primary" type="submit">Publish</button>
      </form>
    </div>
  </div>
  <div class="panel" style="margin-top:24px"><h2>Published announcements</h2>
    ${db.announcements.length?db.announcements.slice().reverse().map(a=>`<div class="ann"><div class="top"><b>${esc(a.title)}</b><span><span class="badge off">${esc(a.audience)}</span> <button class="btn sm danger" data-a="ann-del" data-id="${a.id}">Delete</button></span></div><div class="muted small">${esc(a.body)}</div><div class="muted small">${esc(a.date)}</div></div>`).join(""):empty("No announcements yet.")}
  </div>`);
})();
