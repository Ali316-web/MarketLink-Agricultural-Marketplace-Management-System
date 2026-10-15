/* Content moderation section */
(function(){
"use strict";
const {db,st,esc,money,badge,empty,marketName,farmerName,mapLink}=ML;
ML.mount(()=>{
  const p=db.products,r=db.reviews;
  const flagP=p.filter(x=>x.flag).length,flagR=r.filter(x=>x.flag).length;
  return `
  <div class="page-head"><div><h1>Content moderation</h1><p>Remove listings or reviews that break platform guidelines.</p></div></div>
  <div class="tabs" role="tablist">
    <button role="tab" aria-selected="${st.mod==="products"}" data-a="tab" data-id="products">Product listings (${p.length})</button>
    <button role="tab" aria-selected="${st.mod==="reviews"}" data-a="tab" data-id="reviews">Reviews (${r.length})</button>
  </div>
  ${st.mod==="products"?`<div class="tablewrap">${p.length?`<table><thead><tr><th>Product</th><th>Farmer</th><th>Category</th><th>Price</th><th>Flag</th><th></th></tr></thead><tbody>
    ${p.map(x=>`<tr><td><b>${esc(x.name)}</b></td><td>${esc(farmerName(x.farmer))}</td><td>${esc(x.category)}</td><td>$${x.price.toFixed(2)} / ${esc(x.unit)}</td>
    <td>${x.flag?`<span class="badge bad">${esc(x.flag)}</span>`:`<span class="muted small">None</span>`}</td>
    <td class="act"><button class="btn sm danger" data-a="p-del" data-id="${x.id}">Remove</button></td></tr>`).join("")}</tbody></table>`:empty("No product listings.")}</div>
    <p class="muted small" style="margin-top:10px">${flagP} flagged listing${flagP===1?"":"s"}.</p>`
  :`<div class="tablewrap">${r.length?`<table><thead><tr><th>Review</th><th>Rating</th><th>Flag</th><th></th></tr></thead><tbody>
    ${r.map(x=>`<tr><td><b>${esc(x.customer)}</b> on ${esc(x.target)}<div class="muted small">${esc(x.comment)}</div></td>
    <td><span class="stars">${"★".repeat(x.rating)}${"☆".repeat(5-x.rating)}</span></td>
    <td>${x.flag?`<span class="badge bad">${esc(x.flag)}</span>`:`<span class="muted small">None</span>`}</td>
    <td class="act"><button class="btn sm danger" data-a="r-del" data-id="${x.id}">Remove</button></td></tr>`).join("")}</tbody></table>`:empty("No reviews.")}</div>
    <p class="muted small" style="margin-top:10px">${flagR} flagged review${flagR===1?"":"s"}.</p>`}`;
});
})();
