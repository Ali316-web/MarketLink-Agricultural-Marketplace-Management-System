/* Farmers section */
(function(){
"use strict";
const {db,st,esc,money,badge,empty,marketName,farmerName,mapLink}=ML;
ML.mount(()=>{
  const q=st.fq.toLowerCase();
  const rows=db.farmers.filter(f=>(st.ff==="All"||f.status===st.ff)&&(!q||(f.stall+f.contact+f.email).toLowerCase().includes(q)));
  return `
  <div class="page-head"><div><h1>Farmers</h1><p>Approve registrations before farmers can list products.</p></div></div>
  <div class="toolbar">
    <input type="search" id="fq" placeholder="Search stall, contact or email" value="${esc(st.fq)}" aria-label="Search farmers">
    <select id="ff" aria-label="Filter by status">${["All","Pending","Approved","Suspended"].map(s=>`<option ${st.ff===s?"selected":""}>${s}</option>`).join("")}</select>
  </div>
  <div class="tablewrap">${rows.length?`<table><thead><tr><th>Stall</th><th>Contact</th><th>Market</th><th>Status</th><th>Joined</th><th></th></tr></thead><tbody>
  ${rows.map(f=>`<tr>
    <td><b>${esc(f.stall)}</b></td>
    <td>${esc(f.contact)}<div class="muted small">${esc(f.email)}</div></td>
    <td>${esc(marketName(f.market))}</td>
    <td>${badge(f.status)}</td><td>${esc(f.joined)}</td>
    <td class="act">
      <button class="btn sm" data-a="f-view" data-id="${f.id}">View</button>
      ${f.status!=="Approved"?`<button class="btn sm primary" data-a="f-approve" data-id="${f.id}">${f.status==="Suspended"?"Reinstate":"Approve"}</button>`:""}
      ${f.status!=="Suspended"?`<button class="btn sm danger" data-a="f-suspend" data-id="${f.id}">Suspend</button>`:""}
    </td></tr>`).join("")}</tbody></table>`:empty("No farmers match your filters.")}</div>`;
});
})();
