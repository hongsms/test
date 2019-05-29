var hsm={};hsm.init=function(){hsm.events()};hsm.events=function(){hsm.common()};
hsm.common = function () {
// Share
window._bd_share_config={"share":{}};with(document){0[(getElementsByTagName("head")[0]||body).appendChild(createElement("script")).src="http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion="+~(-new Date()/3600000)]};  

$(".language em").click(function(){
  $(this).siblings("ul").stop().fadeToggle();
});

}
$(function () {
  hsm.init();
});