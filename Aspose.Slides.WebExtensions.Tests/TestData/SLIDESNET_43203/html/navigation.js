

var currentVisiblePage = 0;
var maxVisiblePage = 1;

$(document).ready(function(){
      
    if (false)
        InitNavigation();
    
    if (!true)
        ShowSlide(1);
    else
        currentVisiblePage = 1;
    ChangeThumbSelection(1);
});

function InitNavigation() {
      
    if ($(".navigationArea")[0].scrollHeight <= $(".navigationArea")[0].clientHeight) {
        $(".navigationArea").css('width', '222');    
        $('.navigationArea').addClass('navigationAreaBorder');
    }
    else {
        $(".navigationArea").css('width', '');    
        $('.navigationArea').removeClass('navigationAreaBorder');
    }
    
    SetThumbnailClickHandler();
}

function RemoveThumbnailClickHandler() {
    $(".slideThumb").unbind();
}

function SetThumbnailClickHandler() {
    RemoveThumbnailClickHandler();
    $(".slideThumb").click(function(){ ShowSlide($(this).data('number')); });
}

function ShowSlide(nextVisiblePage) {
    
    if (nextVisiblePage != currentVisiblePage) {         
        ChangeThumbSelection(nextVisiblePage);
        var prevSlideId = '#slide-' + currentVisiblePage;
        var slideId = '#slide-' + nextVisiblePage;
        currentVisiblePage = nextVisiblePage;
        
        if (true) {
            if (true)
                PauseAllEffects(animations);
            
            PrepareAndPlayTransition(slideId, prevSlideId);
        }
        else {
            StackSlides(slideId, prevSlideId);
            $(prevSlideId).hide();
            $(slideId).show();
            
            if (true) {
                PauseAllEffects(prevAnimations);
                RestoreAllEffects(prevAnimations);
                AnimateShapes(slideId);
                PlayNextAnimationsForTarget('slide');
            }
        }
    }
    else {
        
        if (true) {
            PauseAllEffects(animations);
            RestoreAllEffects(animations);
            PrepareAllEffects(animations);
            ResetAnimationIndicies();
            PlayNextAnimationsForTarget('slide');
        }
    }
}

function ChangeThumbSelection(nextVisiblePage) {
    $('.slideThumb').removeClass('navigationSelected');
    $('#slideThumb-' + nextVisiblePage).addClass('navigationSelected');
}