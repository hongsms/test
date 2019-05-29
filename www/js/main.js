var rp = {};
//初始化
rp.init = function(){
	rp.events();
}
rp.events = function(){
	rp.nav();     

}
//导航
rp.nav = function(){
   $('.mobile_btn').click(function(){
	   if($(this).hasClass('open')){
		   $(this).removeClass('open');
	   }else{
		   $(this).addClass('open');
	   }
   });	   
}

$(function(){
	rp.init();
});