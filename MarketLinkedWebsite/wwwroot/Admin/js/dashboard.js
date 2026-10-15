/* Dashboard section */
(function(){
"use strict";
const {db,st,esc,money,badge,empty,marketName,farmerName,mapLink}=ML;
ML.mount(()=>{
  const totalOrders=db.farmers.reduce((s,f)=>s+f.orders,0);
  const pend=db.farmers.filter(f=>f.status==="Pending");
  const max=Math.max(...db.weekly);
  const bars=db.weekly.map((v,i)=>{
    const h=Math.round(v/max*100),x=10+i*48;
    return `<rect x="${x}" y="${120-h}" width="30" height="${h}" rx="3" fill="var(--accent)" opacity="${i===7?1:.45}"/><text x="${x+15}" y="136" text-anchor="middle" font-size="10" fill="var(--muted)">W${i+1}</text><text x="${x+15}" y="${114-h}" text-anchor="middle" font-size="10" fill="var(--text)">${v}</text>`;
  }).join("");
  return `
  <div class="page-head"><div><h1>Dashboard</h1><p>Platform overview</p></div></div>
  <div class="stats">
    <div class="stat"><div class="n">${db.farmers.length}</div><div class="l">Total farmers</div></div>
    <div class="stat"><div class="n">${db.customers.length}</div><div class="l">Total customers</div></div>
    <div class="stat"><div class="n">${db.markets.length}</div><div class="l">Total markets</div></div>
    <div class="stat"><div class="n">${totalOrders.toLocaleString()}</div><div class="l">Total orders</div></div>
  </div>
  <div class="grid2">
    <div class="panel"><h2>Orders per week</h2>
      <svg viewBox="0 0 400 145" width="100%" role="img" aria-label="Bar chart of orders for the last 8 weeks">${bars}</svg>
    </div>
    <div class="panel"><h2>Farmers awaiting approval</h2>
      ${pend.length?`<ul class="list">${pend.map(f=>`<li><div class="t"><b>${esc(f.stall)}</b><div class="muted small">${esc(f.contact)} · applied ${esc(f.joined)}</div></div><button class="btn sm primary" data-a="f-approve" data-id="${f.id}">Approve</button></li>`).join("")}</ul>`:empty("No pending registrations.")}
    </div>
  </div>
  <div class="grid2">
    <div class="panel"><h2>Recent orders</h2>
      <div class="tablewrap"><table><thead><tr><th>Order</th><th>Customer</th><th>Farmer</th><th>Total</th><th>Status</th></tr></thead><tbody>
      ${db.orders.map(o=>`<tr><td>${esc(o.id)}</td><td>${esc(o.customer)}</td><td>${esc(o.farmer)}</td><td>$${o.total.toFixed(2)}</td><td>${badge(o.status)}</td></tr>`).join("")}
      </tbody></table></div>
    </div>
    <div class="panel"><h2>Latest announcements</h2>
      ${db.announcements.length?db.announcements.slice().reverse().slice(0,3).map(a=>`<div class="ann"><div class="top"><b>${esc(a.title)}</b><span class="muted small">${esc(a.audience)}</span></div><div class="muted small">${esc(a.body)}</div></div>`).join(""):empty("Nothing published yet.")}
    </div>
  </div>`;
});
})();
