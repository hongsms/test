$(function(){
	var Heit = $(window).height();
	$(".sild-cn").height(Heit);
	$(window).resize(function(){
		var Heitr = $(window).height();
		$(".sild-cn").height(Heitr);
		})
	$(".retls").hover(function(){
		$(this).children(".honls").stop(false,true).slideToggle();
		});
	$(".folot1 span,.folot2 span").click(function(){
		$(this).siblings(".conls").stop(false,true).slideToggle()
		});
	$(document).click(function (e) {
        if ( !$(e.target).is(".folot1 span")) {
            $(".folot1 .conls").stop(false,true).slideUp();
			}
		if ( !$(e.target).is(".folot2 span")) {
            $(".folot2 .conls").stop(false,true).slideUp();
			}
        });
	$(".conls a").click(function(){
	    var lols = $(this).attr("vals");
		    telo = $(this).text();
		$(this).parent().siblings("input").val(lols).siblings("span").text(telo);
		});
	$(".foltn").each(function(index, element) {
        $(this).children("li:gt(3)").css({borderBottom:"0"});
    });
	$(".G-T").click(function(){
		$('body,html').animate({scrollTop: 0},1000);
		})
	$(".mastSet").hover(function(){
		$(this).children(".blost").stop(false,true).fadeToggle();
		});
	$(".cholo td .moslt > a").click(function(){
		if($(this).hasClass("cur")){
		    $(this).removeClass("cur").parent(".moslt").animate({height:"31px"});
		}else{
			var polis = $(this).siblings("p").height();
		    $(this).addClass("cur").parent(".moslt").animate({height:polis});
			 }
		});
	$(".mlox span").click(function(){
		var lsl = $(this).index();
		$(this).addClass("cur").siblings().removeClass("cur");
		$(".telms .bd").eq(lsl).stop(false,true).fadeIn().siblings().hide();
		});
	$(".left-nav dl dd a.cur").parent("dd").slideDown();
	$(".left-nav dl dt").click(function(){
		$(this).next("dd").stop(false,true).slideToggle().siblings("dd").stop(false,true).slideUp();;
		});
	$(".xxka span").click(function(){
		var lslt = $(this).index();
		$(this).addClass("cur").siblings().removeClass("cur");
		$(".moxln .bd").eq(lslt).stop(false,true).fadeIn().siblings().hide();
		});
});

