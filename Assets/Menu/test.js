var x = 1;
function outer() {
  function inner() {
    var x = 2;
    console.log(x);
  }
  inner();
  console.log(x);
}
outer();
console.log(x);