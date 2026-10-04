document.querySelectorAll('[data-group]').forEach(g=>g.addEventListener('click',e=>{
  const b=e.target.closest('button');if(!b)return;
  g.querySelectorAll('button').forEach(x=>x.classList.remove('on'));b.classList.add('on');
  g.dispatchEvent(new CustomEvent('pick',{detail:b.dataset}));
}));
const flt=document.querySelector('[data-filter]');
if(flt)flt.addEventListener('pick',e=>document.querySelectorAll('.card[data-cat]').forEach(c=>{
  c.hidden=e.detail.cat!=='all'&&c.dataset.cat!==e.detail.cat}));
const tot=document.getElementById('total');
if(tot){let base=345,spice=0;const add=new Set();
  const draw=()=>{let s=base+spice;add.forEach(p=>s+=p);tot.textContent='R '+s.toFixed(2)};
  document.querySelector('[data-spice]').addEventListener('pick',e=>{spice=+e.detail.p||0;draw()});
  document.querySelectorAll('.item[data-p]').forEach(i=>i.addEventListener('click',()=>{
    const p=+i.dataset.p,on=i.classList.toggle('on');i.querySelector('.st').textContent=on?'added':'add';
    on?add.add(p):add.delete(p);draw()}));
  draw()}
const ful=document.querySelector('[data-fulfil]');
if(ful)ful.addEventListener('pick',e=>document.getElementById('fee').textContent=e.detail.fee);
const st=document.querySelector('.stars');
if(st)st.addEventListener('click',e=>{const b=e.target.closest('button');if(!b)return;
  const n=+b.dataset.n;st.querySelectorAll('button').forEach(x=>x.classList.toggle('on',+x.dataset.n<=n))});
document.querySelectorAll('[data-ready]').forEach(b=>b.addEventListener('click',()=>{
  b.closest('.card').style.opacity=.55;b.textContent='Customer notified';b.disabled=true}));
document.querySelectorAll('[data-stock]').forEach(b=>b.addEventListener('click',()=>{
  const off=b.classList.toggle('off');b.textContent=off?'Out of stock (Toggle)':'In stock (Toggle)'}));
document.querySelectorAll('[data-add]').forEach(b=>b.addEventListener('click',()=>{b.textContent='Added ✓'}));
