/* Customers section */
(function(){
"use strict";
const {db,st,esc,money,badge,empty,marketName,farmerName,mapLink}=ML;
ML.mount(()=>{
  const q=st.cq.toLowerCase();
  const rows=db.customers.filter(c=>(st.cf==="All"||c.status===st.cf)&&(!q||(c.name+c.email).toLowerCase().includes(q)));
  return `
  <div class="page-head"><div><h1>Customers</h1><p>Deactivate accounts that violate platform policy.</p></div></div>
  <div class="toolbar">
    <input type="search" id="cq" placeholder="Search name or email" value="${esc(st.cq)}" aria-label="Search customers">
    <select id="cf" aria-label="Filter by status">${["All","Active","Inactive"].map(s=>`<option ${st.cf===s?"selected":""}>${s}</option>`).join("")}</select>
  </div>
  <div class="tablewrap">${rows.length?`<table><thead><tr><th>Name</th><th>Contact</th><th>Address</th><th>Orders</th><th>Status</th><th></th></tr></thead><tbody>
  ${rows.map(c=>`<tr>
    <td><b>${esc(c.name)}</b><div class="muted small">Joined ${esc(c.joined)}</div></td>
    <td>${esc(c.email)}<div class="muted small">${esc(c.phone)}</div></td>
    <td>${esc(c.address)}</td><td>${c.orders}</td><td>${badge(c.status)}</td>
    <td class="act"><button class="btn sm" data-a="c-view" data-id="${c.id}">View</button>
    ${c.status==="Active"?`<button class="btn sm danger" data-a="c-toggle" data-id="${c.id}">Deactivate</button>`:`<button class="btn sm primary" data-a="c-toggle" data-id="${c.id}">Activate</button>`}</td></tr>`).join("")}</tbody></table>`:empty("No customers match your filters.")}</div>`;
});
})();
