/* Markets section */
(function(){
"use strict";
const {db,st,esc,money,badge,empty,marketName,farmerName,mapLink}=ML;
ML.mount(()=>`
  <div class="page-head"><div><h1>Markets</h1><p>Name, address, operating days, timings and map location.</p></div>
  <button class="btn primary" data-a="m-add">Add market</button></div>
  <div class="tablewrap">${db.markets.length?`<table><thead><tr><th>Market</th><th>Days</th><th>Timings</th><th>Coordinates</th><th>Map</th><th></th></tr></thead><tbody>
  ${db.markets.map(m=>`<tr>
    <td><b>${esc(m.name)}</b><div class="muted small">${esc(m.address)}</div></td>
    <td>${esc(m.days.join(", "))}</td><td>${esc(m.open)}–${esc(m.close)}</td>
    <td class="small">${m.lat}, ${m.lng}</td>
    <td><a href="${mapLink(m)}" target="_blank" rel="noopener">${esc(m.provider)}</a></td>
    <td class="act"><button class="btn sm" data-a="m-edit" data-id="${m.id}">Edit</button><button class="btn sm danger" data-a="m-del" data-id="${m.id}">Remove</button></td></tr>`).join("")}</tbody></table>`:empty("No markets yet. Add the first one.")}</div>`);
})();
