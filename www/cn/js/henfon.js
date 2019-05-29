
$(function(){
	var $heights = $(window).height();
	    $widths  = $(window).width();
	if($(window).width()>1200){
		$(".top-s2").css({height:"60px"})
		$(".T-nav > li").hover(function(){
		  $(this).children(".gdlt").stop(false,true).slideToggle().siblings("a").toggleClass("cur");
		});
		
		}else{
			$(".top-s2").height($heights-50);
			$(".T-nav > li > a").click(function(){
			  if($(this).siblings(".gdlt").length>0){
				  $(this).siblings(".gdlt").stop(false,true).slideToggle();
				  $(this).parent().siblings().find(".gdlt").slideUp();
				  return false;
				  }
			  
			});
			
			};
	$(window).resize(function(){
		var mosh = $(window).height();
		if($(window).width()>1200){
			$(".top-s2").css({height:"60px"})
			}else{
				$(".top-s2").height(mosh-50)
				}
		if($(window).width()>1200){
			$(".top-s2").show();
			}else{
				$(".top-s2").hide();
				}
		});
	$(".mob-nav").click(function(){
		$(".top-s2").stop(false,true).fadeToggle();
		});
	$(".prelsc span,.alinkd span").click(function(){
		$(this).siblings().stop(false,true).fadeToggle();
		});
	$(".tab-hd span").click(function(){
		var slis = $(this).index();
		$(this).addClass("cur").siblings().removeClass("cur");
		$(this).parent().siblings(".tab-bd").children(".bd").eq(slis).stop(false,true).fadeIn().siblings().hide();
		});
	
	$(".alinkd span").text($(".alinkd p a.cur").text())
	$(".enlmcs dt:eq(0)").addClass("cur").next("dd").show();
	$(".enlmcs dt").click(function(){
		$(this).addClass("cur").siblings("dt").removeClass("cur");
		$(this).next("dd").stop(false,true).slideDown().siblings("dd").stop(false,true).slideUp();
		})
	$(".go-top").click(function(){
		$("html,body").animate({scrollTop:"0"})
		})
	$(".lbcels li p a.on").parent().show().siblings("a").addClass("cur")
	$(".lbcels li > a").click(function(){
		if($(this).siblings().length>0){
			$(this).addClass("cur").siblings("p").stop(false,true).slideDown().parent().siblings("li").children("a").removeClass("cur").siblings("p").stop(false,true).slideUp();
			return false
			}
		});
	$(".zclis h3").click(function(){
		if($(window).width()<=1200){
			$(this).siblings().stop(false,true).slideToggle();
			}
		})
	$(".sl-date span").each(function(index, element) {
        $(this).html($(this).siblings(".mons").children("a.on").html());
    });
	$(".sl-date span").click(function(){
		$(this).siblings(".mons").stop(false,true).slideToggle();
		})
	$(".choslt dl dd a.more").click(function(){
		if($(this).hasClass("op")){
			$(this).removeClass("op").siblings("p").css({height:"22px"})
			}else{
				$(this).addClass("op").siblings("p").css({height:"auto"})
				}
		})
	
})


















