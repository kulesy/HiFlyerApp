function switchFormFocus(left, right) {
    let rightWidth = right.getBoundingClientRect().width;
    let leftWidth = left.getBoundingClientRect().width;
    if (leftWidth > rightWidth) {
        right.style.transform = 'translateX(-' + leftWidth + 'px)';
        right.children[0].style.transition = 'opacity 0.1s ease-in-out';
        left.children[0].style.transition = 'opacity 0.1s ease-in-out';
        right.children[0].style.opacity = '0';
        left.children[0].style.opacity = '0';
    }
    else if (rightWidth > leftWidth) {
        left.style.transform = 'translateX(' + rightWidth + 'px)';
        right.children[0].style.transition = 'opacity 0.1s ease-in-out';
        left.children[0].style.transition = 'opacity 0.1s ease-in-out';
        left.children[0].style.opacity = '0';
        right.children[0].style.opacity = '0';
    }
}

function opacityToggle(left, right) {
    let rightWidth = right.getBoundingClientRect().width;
    let leftWidth = left.getBoundingClientRect().width;
    if (leftWidth > rightWidth) {
        right.children[0].style.transition = 'opacity 0.1s ease-in-out';
        left.children[0].style.transition = 'opacity 0.1s ease-in-out';
        right.children[0].style.opacity = '1';
        left.children[0].style.opacity = '1';
    }
    else if (rightWidth > leftWidth) {
        right.children[0].style.transition = 'opacity 0.1s ease-in-out';
        left.children[0].style.transition = 'opacity 0.1s ease-in-out';
        right.children[0].style.opacity = '1';
        left.children[0].style.opacity = '1';
    }
    
}

async function moveSlide(track, indicators, direction) {
    var trackArray = Array.from(track.children);
    var currentSlide = track.querySelector('.slide-current');
    var currentSlideIndex = trackArray.indexOf(currentSlide);
    var nextSlideIndex = currentSlideIndex + 1;
    var prevSlideIndex = currentSlideIndex - 1;
    if (currentSlideIndex == 0) {
        prevSlideIndex = trackArray.length - 1;
    }
    if (currentSlideIndex == trackArray.length - 1) {
        nextSlideIndex = 0;
    }

    await performSlide(track, indicators, direction, currentSlideIndex, nextSlideIndex, prevSlideIndex);
}

async function moveToSlide(track, indicators, targetIndex) {
    var trackArray = Array.from(track.children);
    var currentSlide = track.querySelector('.slide-current');
    var currentSlideIndex = trackArray.indexOf(currentSlide);

    if (currentSlideIndex < targetIndex) {
        await performSlide(track, indicators, 'foward', currentSlideIndex, targetIndex, 0);
    }
    else if (currentSlideIndex > targetIndex) {
        await performSlide(track, indicators, 'backward', currentSlideIndex, 0, targetIndex);
    }
    
}


async function performSlide(track, indicators, direction, currentSlideIndex, nextSlideIndex, prevSlideIndex) {
    var trackArray = Array.from(track.children);
    if (direction == 'foward') {
        activateIndicator(indicators, nextSlideIndex);
        trackArray[nextSlideIndex].classList.add("slide-next");
        trackArray[nextSlideIndex].classList.add("slide-end");
        await delay(1);
        trackArray[nextSlideIndex].classList.remove("slide-end");
        trackArray[currentSlideIndex].classList.add("slide-start");
        await delay(600);
        trackArray[nextSlideIndex].classList.remove("slide-next");
        trackArray[currentSlideIndex].classList.remove("slide-current");
        trackArray[currentSlideIndex].classList.remove("slide-start");
        trackArray[nextSlideIndex].classList.add("slide-current");
    }
    else if (direction == 'backward') {
        activateIndicator(indicators, prevSlideIndex);
        trackArray[prevSlideIndex].classList.add("slide-prev");
        trackArray[prevSlideIndex].classList.add("slide-start");
        await delay(1);
        trackArray[prevSlideIndex].classList.remove("slide-start");
        trackArray[currentSlideIndex].classList.add("slide-end");
        await delay(600);
        trackArray[prevSlideIndex].classList.remove("slide-prev");
        trackArray[currentSlideIndex].classList.remove("slide-current");
        trackArray[currentSlideIndex].classList.remove("slide-end");
        trackArray[prevSlideIndex].classList.add("slide-current");
    }
}

function activateIndicator(indicators, targetIndex) {
    var indicatorsArray = Array.from(indicators.children);
    indicatorsArray.forEach(e => {
        if (e == indicatorsArray[targetIndex]) {
            e.classList.add("indicator-current")
        }
        else if (e != indicatorsArray[targetIndex]) {
            e.classList.remove("indicator-current")
        }
    })
}

async function menuToggle(isOn, navigation) {
    const navLeft = navigation.querySelector(".navigation__list-left");
    const navRight = navigation.querySelector(".navigation__list-right");
    if (isOn == true) {

        navLeft.classList.add("nav-visible");
        navRight.classList.add("nav-visible");
        await delay(1);
        navLeft.classList.add("nav-slide");
        navRight.classList.add("nav-slide");
    }
    else if (isOn == false) {
        navLeft.classList.remove("nav-slide");
        navRight.classList.remove("nav-slide");
        await delay(400);
        navLeft.classList.remove("nav-visible");
        navRight.classList.remove("nav-visible");
    }
}

function cartToggle(isOn) {
    var cart = document.getElementById("cart");
    if (isOn == true) {

        cart.classList.add("cart-active");
    }
    else if (isOn == false) {
        cart.classList.remove("cart-active");
    }
}

function trackSlide(track, direction) {
    var visibleTrackWidth = track.parentNode.offsetWidth;
    var trackArray = Array.from(track.children);
    var currentSlide = track.querySelector(".slide-current");
    var currentSlideWidth = currentSlide.offsetWidth;
    var currentSlideIndex = trackArray.indexOf(currentSlide);
    var visibleSlides = visibleTrackWidth / currentSlideWidth;
    var allowedSlides = trackArray.length - visibleSlides;

    var numberToTimes = trackArray.indexOf(currentSlide) + 1;
        console.log(trackArray.length);
        console.log(visibleSlides);
    if (trackArray.length > visibleSlides) {
        if (direction == 'forward') {
            console.log(currentSlideIndex);
            console.log(allowedSlides);
            if (currentSlideIndex < allowedSlides) {
                track.style.transform = 'translateX(-' + currentSlideWidth * numberToTimes + 'px)';
                currentSlide.classList.remove("slide-current");
                trackArray[currentSlideIndex + 1].classList.add("slide-current");
            }
            else if (currentSlideIndex == allowedSlides) {
                track.style.transform = 'translateX(0px)';
                currentSlide.classList.remove("slide-current");
                trackArray[0].classList.add("slide-current");
            }
        }
        if (direction == 'backward') {
            if ((currentSlideIndex <= allowedSlides) && (currentSlideIndex > 0)) {
                track.style.transform = 'translateX(-' + currentSlideWidth * (currentSlideIndex - 1) + 'px)';
                currentSlide.classList.remove("slide-current");
                trackArray[currentSlideIndex - 1].classList.add("slide-current");
            }
            else if (currentSlideIndex == 0) {
                track.style.transform = 'translateX(-' + currentSlideWidth * allowedSlides + 'px)';
                currentSlide.classList.remove("slide-current");
                trackArray[allowedSlides].classList.add("slide-current");
            }
        }
    }
}

function toggleMainSlide(track, targetIndex) {
    var trackArray = Array.from(track.children);
    var mainSlide = track.querySelector(".slide-main");
    mainSlide.classList.remove("slide-main");
    trackArray[targetIndex].classList.add("slide-main");
    return targetIndex;
}

function closeError(errorList, targetErrorIndex) {
    targetErrorIndex += 1;
    var errorListArray = Array.from(errorList.children);
    var targetError = errorListArray[targetErrorIndex];
    targetError.style.display = "none";
}

function delay(n) {
    return new Promise(function (resolve) {
        setTimeout(resolve, n);
    });
}

document.addEventListener('DOMContentLoaded', function () {
    document.addEventListener("click", function (e) {
        var element = e.target;
        if (element.classList.value.includes("cart") == false) {
            cartToggle(false);
        }
    });
});