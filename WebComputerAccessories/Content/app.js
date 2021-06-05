const $$ = document.querySelector.bind(document);
let backToTop = () => {
    window.onscroll = () => {
        const scrollTop = $$('.back-to-top');
        this.scrollY > 100 ? scrollTop.style.display = "block" : scrollTop.style.display = "none";
    }
}
backToTop();


var app = {

    js: function () {

        let navBarMobile = () => {
            window.$(document).ready(() => {
                window.$('#hamburger-menu').click(() => {
                    window.$('#hamburger-menu').toggleClass('active');
                    window.$('#nav-menu').toggleClass('active');
                });
            });
        }
        navBarMobile();

    },

    start: function() {
        this.js();
    }
}
app.start();

