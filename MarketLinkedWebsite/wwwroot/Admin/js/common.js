/* MarketLink Admin - shared code for the admin pages: sample data (persisted in localStorage), helpers, dialogs, actions. */
(function(){
"use strict";
const $=(s,r=document)=>r.querySelector(s);
const esc=s=>String(s??"").replace(/[&<>"']/g,c=>({"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#39;"}[c]));
const money=n=>"$"+Number(n).toLocaleString("en-US",{maximumFractionDigits:0});
const now=()=>new Date().toISOString().slice(0,16).replace("T"," ");
const DAYS=["Mon","Tue","Wed","Thu","Fri","Sat","Sun"];

/* ---------- sample data (in memory only) ---------- */
const seed={
  markets:[
    {id:1,name:"Riverside Sunday Market",address:"12 Riverside Walk, Springfield",days:["Sun"],open:"08:00",close:"14:00",lat:39.7817,lng:-89.6501,provider:"OpenStreetMap",embed:""},
    {id:2,name:"Downtown Green Square",address:"200 Main Street, Springfield",days:["Wed","Sat"],open:"07:30",close:"13:00",lat:39.8003,lng:-89.6437,provider:"Google Maps",embed:""},
    {id:3,name:"Oak Hill Farmers Market",address:"45 Oak Hill Road, Riverton",days:["Sat"],open:"08:00",close:"12:30",lat:39.8367,lng:-89.5387,provider:"OpenStreetMap",embed:""},
    {id:4,name:"Lakeside Morning Market",address:"8 Lake Drive, Chatham",days:["Tue","Fri"],open:"09:00",close:"13:00",lat:39.6764,lng:-89.7043,provider:"Google Maps",embed:""},
    {id:5,name:"Harvest Court",address:"77 Barn Lane, Jerome",days:["Thu"],open:"10:00",close:"15:00",lat:39.7645,lng:-89.6862,provider:"OpenStreetMap",embed:""}
  ],
  farmers:[
    {id:1,stall:"Green Acre Produce",contact:"Maria Lopez",phone:"555-0101",email:"maria@greenacre.com",market:1,status:"Approved",joined:"2026-01-14",orders:214,revenue:6120},
    {id:2,stall:"Sunrise Dairy",contact:"Tom Weber",phone:"555-0102",email:"tom@sunrisedairy.com",market:2,status:"Approved",joined:"2026-01-20",orders:188,revenue:7340},
    {id:3,stall:"Honey Hollow Apiary",contact:"Priya Nair",phone:"555-0103",email:"priya@honeyhollow.com",market:3,status:"Approved",joined:"2026-02-03",orders:97,revenue:2890},
    {id:4,stall:"Stone Oven Bakery",contact:"Luca Ferri",phone:"555-0104",email:"luca@stoneoven.com",market:2,status:"Approved",joined:"2026-02-11",orders:176,revenue:4310},
    {id:5,stall:"Berry Patch Farm",contact:"Anna Kim",phone:"555-0105",email:"anna@berrypatch.com",market:4,status:"Approved",joined:"2026-03-01",orders:121,revenue:3475},
    {id:6,stall:"Meadow Eggs & Poultry",contact:"Jamal Reed",phone:"555-0106",email:"jamal@meadoweggs.com",market:1,status:"Suspended",joined:"2026-03-09",orders:44,revenue:960},
    {id:7,stall:"Valley Roots",contact:"Sofia Ortiz",phone:"555-0107",email:"sofia@valleyroots.com",market:5,status:"Approved",joined:"2026-04-02",orders:83,revenue:2205},
    {id:8,stall:"Orchard Lane Apples",contact:"Ben Carter",phone:"555-0108",email:"ben@orchardlane.com",market:3,status:"Pending",joined:"2026-09-18",orders:0,revenue:0},
    {id:9,stall:"Fern & Field Herbs",contact:"Hana Sato",phone:"555-0109",email:"hana@fernfield.com",market:4,status:"Pending",joined:"2026-09-21",orders:0,revenue:0},
    {id:10,stall:"Copper Creek Cheese",contact:"Ivan Petrov",phone:"555-0110",email:"ivan@coppercreek.com",market:5,status:"Pending",joined:"2026-09-23",orders:0,revenue:0}
  ],
  customers:[
    {id:1,name:"Emily Clark",email:"emily.c@mail.com",phone:"555-0201",address:"14 Elm St, Springfield",status:"Active",joined:"2026-01-18",orders:22},
    {id:2,name:"Noah Bennett",email:"noah.b@mail.com",phone:"555-0202",address:"9 Pine Ave, Springfield",status:"Active",joined:"2026-01-25",orders:17},
    {id:3,name:"Olivia Grant",email:"olivia.g@mail.com",phone:"555-0203",address:"71 Cedar Rd, Riverton",status:"Active",joined:"2026-02-07",orders:31},
    {id:4,name:"Liam Foster",email:"liam.f@mail.com",phone:"555-0204",address:"3 Birch Ln, Chatham",status:"Inactive",joined:"2026-02-19",orders:5},
    {id:5,name:"Ava Morgan",email:"ava.m@mail.com",phone:"555-0205",address:"120 Maple Dr, Jerome",status:"Active",joined:"2026-03-04",orders:12},
    {id:6,name:"Ethan Brooks",email:"ethan.b@mail.com",phone:"555-0206",address:"55 Willow Ct, Springfield",status:"Active",joined:"2026-03-22",orders:9},
    {id:7,name:"Mia Sullivan",email:"mia.s@mail.com",phone:"555-0207",address:"18 Aspen Way, Riverton",status:"Inactive",joined:"2026-04-10",orders:2},
    {id:8,name:"Lucas Hayes",email:"lucas.h@mail.com",phone:"555-0208",address:"6 Spruce St, Springfield",status:"Active",joined:"2026-05-15",orders:14}
  ],
  products:[
    {id:1,name:"Heirloom tomatoes",farmer:1,category:"Vegetables",price:4.5,unit:"lb",flag:""},
    {id:2,name:"Raw whole milk",farmer:2,category:"Dairy",price:5,unit:"half gal",flag:""},
    {id:3,name:"Wildflower honey",farmer:3,category:"Pantry",price:9,unit:"jar",flag:"Misleading 'organic' claim"},
    {id:4,name:"Sourdough loaf",farmer:4,category:"Baked goods",price:7,unit:"loaf",flag:""},
    {id:5,name:"Strawberries",farmer:5,category:"Fruits",price:6,unit:"pint",flag:""},
    {id:6,name:"Free-range eggs",farmer:6,category:"Dairy",price:6.5,unit:"dozen",flag:"Reported by 3 customers"},
    {id:7,name:"Carrot bunch",farmer:7,category:"Vegetables",price:3,unit:"bunch",flag:""},
    {id:8,name:"Miracle cure herb mix",farmer:7,category:"Pantry",price:25,unit:"bag",flag:"Unverified health claims"}
  ],
  reviews:[
    {id:1,customer:"Emily Clark",target:"Green Acre Produce",rating:5,comment:"Tomatoes were perfect and pickup was quick.",flag:""},
    {id:2,customer:"Noah Bennett",target:"Sunrise Dairy",rating:4,comment:"Great milk, slot was a little late.",flag:""},
    {id:3,customer:"Liam Foster",target:"Stone Oven Bakery",rating:1,comment:"Terrible people, do not go here. (abusive language removed)",flag:"Abusive language"},
    {id:4,customer:"Ava Morgan",target:"Berry Patch Farm",rating:5,comment:"Best strawberries this season.",flag:""},
    {id:5,customer:"Lucas Hayes",target:"Honey Hollow Apiary",rating:2,comment:"Visit my site for cheap deals http://spam.example",flag:"Spam link"},
    {id:6,customer:"Olivia Grant",target:"Valley Roots",rating:4,comment:"Fresh and fairly priced.",flag:""}
  ],
  orders:[
    {id:"ML-1042",customer:"Emily Clark",farmer:"Green Acre Produce",total:32.5,status:"Completed"},
    {id:"ML-1043",customer:"Noah Bennett",farmer:"Sunrise Dairy",total:18,status:"Ready for pickup"},
    {id:"ML-1044",customer:"Olivia Grant",farmer:"Honey Hollow Apiary",total:27,status:"Accepted"},
    {id:"ML-1045",customer:"Ava Morgan",farmer:"Berry Patch Farm",total:21,status:"Placed"},
    {id:"ML-1046",customer:"Lucas Hayes",farmer:"Stone Oven Bakery",total:14,status:"Cancelled"},
    {id:"ML-1047",customer:"Ethan Brooks",farmer:"Valley Roots",total:11.5,status:"Placed"}
  ],
  weekly:[142,158,171,149,186,203,198,221],
  categories:["Vegetables","Fruits","Dairy","Baked goods","Pantry","Meat & poultry","Flowers"],
  announcements:[
    {id:1,title:"Harvest season hours",body:"Most markets open 30 minutes earlier through October.",audience:"All users",date:"2026-09-15 09:00"},
    {id:2,title:"Update your weekly stock",body:"Please refresh your stock template before Friday's cutoff.",audience:"Farmers",date:"2026-09-20 16:30"}
  ],
  reports:[
    {id:1,type:"Orders summary",by:"Admin",at:"2026-09-01 10:12"},
    {id:2,type:"Revenue by market",by:"Admin",at:"2026-09-10 14:40"},
    {id:3,type:"Most active farmers",by:"Admin",at:"2026-09-18 08:05"}
  ]
};
const STORE="marketlink_admin_db_v1";
function loadDb(){try{const s=localStorage.getItem(STORE);if(s)return JSON.parse(s)}catch(_){}return JSON.parse(JSON.stringify(seed))}
function saveDb(){try{localStorage.setItem(STORE,JSON.stringify(db))}catch(_){}}
const db=loadDb();
const nid=a=>a.reduce((m,x)=>Math.max(m,typeof x.id==="number"?x.id:0),0)+1;
const marketName=id=>(db.markets.find(m=>m.id===id)||{name:"Unassigned"}).name;
const farmerName=id=>(db.farmers.find(f=>f.id===id)||{stall:"Unknown"}).stall;
const mapLink=m=>m.provider==="Google Maps"
  ?`https://www.google.com/maps?q=${m.lat},${m.lng}`
  :`https://www.openstreetmap.org/?mlat=${m.lat}&mlon=${m.lng}#map=16/${m.lat}/${m.lng}`;

/* ---------- helpers ---------- */
function badge(s){
  const map={Approved:"",Active:"",Completed:"",Pending:"warn",Placed:"warn",Accepted:"warn","Ready for pickup":"",Suspended:"bad",Inactive:"off",Cancelled:"off"};
  return `<span class="badge ${map[s]||""}">${esc(s)}</span>`;
}
function toast(msg){const t=$("#toast");t.textContent=msg;t.classList.add("on");clearTimeout(toast.t);toast.t=setTimeout(()=>t.classList.remove("on"),2400)}
function openDlg(html){const d=$("#dlg");d.innerHTML=`<div class="dlg">${html}</div>`;if(!d.open)d.showModal()}
function closeDlg(){const d=$("#dlg");if(d.open)d.close()}
function ask(msg,label,fn){
  st.confirm=fn;
  openDlg(`<h2>${esc(label)}?</h2><p class="muted">${esc(msg)}</p><div class="foot"><button class="btn" data-a="close">Cancel</button><button class="btn danger" data-a="yes">${esc(label)}</button></div>`);
}
function empty(t){return `<div class="empty">${esc(t)}</div>`}

/* ---------- modals ---------- */
function farmerModal(f){
  openDlg(`<h2>${esc(f.stall)}</h2>
  <dl class="kv"><dt>Status</dt><dd>${badge(f.status)}</dd><dt>Contact person</dt><dd>${esc(f.contact)}</dd><dt>Phone</dt><dd>${esc(f.phone)}</dd><dt>Email</dt><dd>${esc(f.email)}</dd><dt>Market</dt><dd>${esc(marketName(f.market))}</dd><dt>Joined</dt><dd>${esc(f.joined)}</dd><dt>Orders</dt><dd>${f.orders}</dd><dt>Revenue</dt><dd>${money(f.revenue)}</dd></dl>
  <div class="foot"><button class="btn" data-a="close">Close</button></div>`);
}
function customerModal(c){
  openDlg(`<h2>${esc(c.name)}</h2>
  <dl class="kv"><dt>Status</dt><dd>${badge(c.status)}</dd><dt>Email</dt><dd>${esc(c.email)}</dd><dt>Phone</dt><dd>${esc(c.phone)}</dd><dt>Address</dt><dd>${esc(c.address)}</dd><dt>Joined</dt><dd>${esc(c.joined)}</dd><dt>Orders</dt><dd>${c.orders}</dd></dl>
  <div class="foot"><button class="btn" data-a="close">Close</button></div>`);
}
function marketModal(m){
  const e=!!m; m=m||{name:"",address:"",days:[],open:"08:00",close:"13:00",lat:"",lng:"",provider:"OpenStreetMap",embed:""};
  openDlg(`<h2>${e?"Edit market":"Add market"}</h2>
  <form id="market-form" data-id="${e?m.id:""}" novalidate>
    <div class="field"><label for="m-name">Market name</label><input id="m-name" type="text" value="${esc(m.name)}"></div>
    <div class="field"><label for="m-addr">Address</label><input id="m-addr" type="text" value="${esc(m.address)}"></div>
    <div class="field"><label>Operating days</label><div class="checks">${DAYS.map(d=>`<label><input type="checkbox" name="day" value="${d}" ${m.days.includes(d)?"checked":""}>${d}</label>`).join("")}</div></div>
    <div class="row"><div class="field"><label for="m-open">Opens</label><input id="m-open" type="time" value="${m.open}"></div><div class="field"><label for="m-close">Closes</label><input id="m-close" type="time" value="${m.close}"></div></div>
    <div class="row"><div class="field"><label for="m-lat">Latitude</label><input id="m-lat" type="number" step="any" value="${m.lat}"></div><div class="field"><label for="m-lng">Longitude</label><input id="m-lng" type="number" step="any" value="${m.lng}"></div></div>
    <div class="field"><label for="m-prov">Map provider</label><select id="m-prov"><option ${m.provider==="OpenStreetMap"?"selected":""}>OpenStreetMap</option><option ${m.provider==="Google Maps"?"selected":""}>Google Maps</option></select></div>
    <div class="field"><label for="m-embed">Embedded map link (optional)</label><input id="m-embed" type="text" value="${esc(m.embed)}" placeholder="https://"></div>
    <div class="err" id="m-err" role="alert"></div>
    <div class="foot" style="margin-top:8px"><button type="button" class="btn" data-a="close">Cancel</button><button class="btn primary" type="submit">Save market</button></div>
  </form>`);
}

/* ---------- state ---------- */
const st={fq:"",ff:"All",cq:"",cf:"All",mod:"products",confirm:null};
const NAV=[
  ["dashboard","Dashboard"],["farmers","Farmers"],["customers","Customers"],["markets","Markets"],
  ["moderation","Content moderation"],["reports","Reports"],["settings","System configuration"]
];
let view=null;
function renderNav(){
  const cur=document.body.dataset.page;
  const pend=db.farmers.filter(f=>f.status==="Pending").length;
  $("#nav").innerHTML=NAV.map(([k,l])=>`<a href="${k}.html" ${cur===k?'aria-current="page"':""}>${l}${k==="farmers"&&pend?`<span class="count">${pend}</span>`:""}</a>`).join("");
}
function render(){
  saveDb();
  if(!view)return;
  renderNav();
  $("#view").innerHTML=view();
}
function mount(fn){view=fn;render()}
window.ML={db,st,esc,money,badge,empty,marketName,farmerName,mapLink,toast,openDlg,closeDlg,ask,mount,render};
/* ---------- actions ---------- */
document.addEventListener("click",e=>{
  const el=e.target.closest("[data-a]"); if(!el)return;
  const a=el.dataset.a,id=el.dataset.id,n=Number(id);
  switch(a){
    case"menu":$("#side").classList.toggle("open");break;
    case"close":closeDlg();break;
    case"yes":{const f=st.confirm;st.confirm=null;closeDlg();if(f)f();break}
    case"f-view":farmerModal(db.farmers.find(x=>x.id===n));break;
    case"f-approve":{const f=db.farmers.find(x=>x.id===n);const was=f.status;f.status="Approved";render();toast(was==="Suspended"?"Farmer reinstated":"Farmer approved");break}
    case"f-suspend":{const f=db.farmers.find(x=>x.id===n);ask(`${f.stall} will not be able to list products until reinstated.`,"Suspend",()=>{f.status="Suspended";render();toast("Farmer suspended")});break}
    case"c-view":customerModal(db.customers.find(x=>x.id===n));break;
    case"c-toggle":{const c=db.customers.find(x=>x.id===n);
      if(c.status==="Active")ask(`${c.name} will lose access to their account.`,"Deactivate",()=>{c.status="Inactive";render();toast("Account deactivated")});
      else{c.status="Active";render();toast("Account activated")}break}
    case"m-add":marketModal();break;
    case"m-edit":marketModal(db.markets.find(x=>x.id===n));break;
    case"m-del":{const m=db.markets.find(x=>x.id===n);ask(`${m.name} will be removed from the platform.`,"Remove",()=>{db.markets=db.markets.filter(x=>x.id!==n);render();toast("Market removed")});break}
    case"tab":st.mod=id;render();break;
    case"p-del":{const p=db.products.find(x=>x.id===n);ask(`"${p.name}" will be removed from ${farmerName(p.farmer)}'s listings.`,"Remove",()=>{db.products=db.products.filter(x=>x.id!==n);render();toast("Listing removed")});break}
    case"r-del":ask("This review will be permanently removed.","Remove",()=>{db.reviews=db.reviews.filter(x=>x.id!==n);render();toast("Review removed")});break;
    case"rep-new":openDlg(`<h2>Generate report</h2><form id="rep-form"><div class="field"><label for="r-type">Report type</label><select id="r-type"><option>Orders summary</option><option>Revenue by market</option><option>Most active farmers</option><option>Full platform report</option></select></div><div class="foot"><button type="button" class="btn" data-a="close">Cancel</button><button class="btn primary" type="submit">Generate</button></div></form>`);break;
    case"cat-edit":{const cur=db.categories[n];openDlg(`<h2>Rename category</h2><form id="cat-edit-form" data-id="${n}"><div class="field"><label for="ce-name">Category name</label><input id="ce-name" type="text" value="${esc(cur)}"></div><div class="err" id="ce-err" role="alert"></div><div class="foot"><button type="button" class="btn" data-a="close">Cancel</button><button class="btn primary" type="submit">Save category</button></div></form>`);break}
    case"cat-del":{const c=db.categories[n];ask(`"${c}" will be removed from the category list.`,"Delete",()=>{db.categories.splice(n,1);render();toast("Category deleted")});break}
    case"ann-del":ask("This announcement will be removed for all users.","Delete",()=>{db.announcements=db.announcements.filter(x=>x.id!==n);render();toast("Announcement deleted")});break;
  }
});

document.addEventListener("submit",e=>{
  e.preventDefault();
  const f=e.target;
  if(f.id==="market-form"){
    const days=[...f.querySelectorAll('[name=day]:checked')].map(x=>x.value);
    const m={name:$("#m-name").value.trim(),address:$("#m-addr").value.trim(),days,open:$("#m-open").value,close:$("#m-close").value,lat:parseFloat($("#m-lat").value),lng:parseFloat($("#m-lng").value),provider:$("#m-prov").value,embed:$("#m-embed").value.trim()};
    const err=$("#m-err");
    if(!m.name||!m.address){err.textContent="Enter a market name and address.";return}
    if(!days.length){err.textContent="Select at least one operating day.";return}
    if(!m.open||!m.close||m.open>=m.close){err.textContent="Closing time must be later than opening time.";return}
    if(isNaN(m.lat)||m.lat<-90||m.lat>90||isNaN(m.lng)||m.lng<-180||m.lng>180){err.textContent="Enter valid coordinates (latitude −90 to 90, longitude −180 to 180).";return}
    if(f.dataset.id){Object.assign(db.markets.find(x=>x.id===Number(f.dataset.id)),m);toast("Market saved")}
    else{m.id=nid(db.markets);db.markets.push(m);toast("Market added")}
    closeDlg();render();
  }
  else if(f.id==="rep-form"){
    db.reports.push({id:nid(db.reports),type:$("#r-type").value,by:"Admin",at:now()});
    closeDlg();render();toast("Report generated");
  }
  else if(f.id==="cat-form"){
    const v=$("#cat-name").value.trim();
    if(!v){$("#cat-err").textContent="Enter a category name.";return}
    if(db.categories.some(c=>c.toLowerCase()===v.toLowerCase())){$("#cat-err").textContent="That category already exists.";return}
    db.categories.push(v);render();toast("Category added");
  }
  else if(f.id==="cat-edit-form"){
    const v=$("#ce-name").value.trim(),i=Number(f.dataset.id);
    if(!v){$("#ce-err").textContent="Enter a category name.";return}
    if(db.categories.some((c,j)=>j!==i&&c.toLowerCase()===v.toLowerCase())){$("#ce-err").textContent="That category already exists.";return}
    db.categories[i]=v;closeDlg();render();toast("Category saved");
  }
  else if(f.id==="ann-form"){
    const t=$("#a-title").value.trim(),b=$("#a-body").value.trim();
    if(!t||!b){$("#a-err").textContent="Add a title and a message before publishing.";return}
    db.announcements.push({id:nid(db.announcements),title:t,body:b,audience:$("#a-aud").value,date:now()});
    render();toast("Announcement published");
  }
});

/* live search / filters (keep focus while re-rendering) */
document.addEventListener("input",e=>{
  const id=e.target.id;
  if(id==="fq"||id==="cq"){
    st[id]=e.target.value;const pos=e.target.selectionStart;render();
    const el=$("#"+id);el.focus();try{el.setSelectionRange(pos,pos)}catch(_){}
  }
});
document.addEventListener("change",e=>{
  if(e.target.id==="ff"){st.ff=e.target.value;render();$("#ff").focus()}
  if(e.target.id==="cf"){st.cf=e.target.value;render();$("#cf").focus()}
});
$("#dlg").addEventListener("click",e=>{if(e.target===$("#dlg"))closeDlg()});
})();
