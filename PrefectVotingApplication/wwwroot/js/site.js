// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//footer, only appears at bottom
window.addEventListener('scroll', function () {
    const footer = document.getElementById('bottom-footer');
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
        footer.classList.add('show');
    } else {
        footer.classList.remove('show');
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const message = document.getElementById("success-message");
    if (message) {
        //this is for notification saying "successfully voted"
        setTimeout(() => {
            message.style.transition = "opacity 0.5s ease"; // fade out after 2 seconds
            message.style.opacity = "0";
            setTimeout(() => message.remove(), 500); // remove after fade
        }, 2000);
    }
});