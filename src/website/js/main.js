// SchoolConnect Website JavaScript
// Handles interactivity for the marketing website

document.addEventListener('DOMContentLoaded', function() {
  // Initialize all components
  initMobileMenu();
  initStickyHeader();
  initFAQAccordion();
  initSmoothScroll();
  initFormValidation();
  initAnimateOnScroll();
  initCookieBanner();
});

// Mobile Navigation Toggle
function initMobileMenu() {
  const mobileMenuBtn = document.getElementById('mobile-menu-btn');
  const mobileMenu = document.getElementById('mobile-menu');
  const mobileMenuClose = document.getElementById('mobile-menu-close');
  
  if (mobileMenuBtn && mobileMenu) {
    mobileMenuBtn.addEventListener('click', function() {
      mobileMenu.classList.add('active');
      document.body.style.overflow = 'hidden';
    });
  }
  
  if (mobileMenuClose && mobileMenu) {
    mobileMenuClose.addEventListener('click', function() {
      mobileMenu.classList.remove('active');
      document.body.style.overflow = '';
    });
  }
  
  // Close mobile menu when clicking on a link
  if (mobileMenu) {
    const mobileLinks = mobileMenu.querySelectorAll('a');
    mobileLinks.forEach(link => {
      link.addEventListener('click', function() {
        mobileMenu.classList.remove('active');
        document.body.style.overflow = '';
      });
    });
  }
}

// Sticky Header on Scroll
function initStickyHeader() {
  const header = document.getElementById('main-header');
  
  if (header) {
    let lastScroll = 0;
    
    window.addEventListener('scroll', function() {
      const currentScroll = window.pageYOffset;
      
      if (currentScroll > 100) {
        header.classList.add('sticky-header');
      } else {
        header.classList.remove('sticky-header');
      }
      
      lastScroll = currentScroll;
    });
  }
}

// FAQ Accordion Functionality
function initFAQAccordion() {
  const faqQuestions = document.querySelectorAll('.faq-question');
  
  faqQuestions.forEach(question => {
    question.addEventListener('click', function() {
      const answer = this.nextElementSibling;
      const icon = this.querySelector('.faq-icon');
      
      // Toggle active state
      const isActive = answer.classList.contains('active');
      
      // Close all answers
      document.querySelectorAll('.faq-answer').forEach(ans => {
        ans.classList.remove('active');
      });
      
      document.querySelectorAll('.faq-icon').forEach(ic => {
        ic.classList.remove('active');
      });
      
      // Open clicked answer if it wasn't active
      if (!isActive) {
        answer.classList.add('active');
        if (icon) icon.classList.add('active');
      }
    });
  });
}

// Smooth Scroll for Anchor Links
function initSmoothScroll() {
  const anchorLinks = document.querySelectorAll('a[href^="#"]');
  
  anchorLinks.forEach(link => {
    link.addEventListener('click', function(e) {
      const href = this.getAttribute('href');
      
      // Ignore # only links
      if (href === '#') return;
      
      const target = document.querySelector(href);
      
      if (target) {
        e.preventDefault();
        const headerHeight = document.getElementById('main-header')?.offsetHeight || 80;
        const targetPosition = target.offsetTop - headerHeight;
        
        window.scrollTo({
          top: targetPosition,
          behavior: 'smooth'
        });
      }
    });
  });
}

// Form Validation
function initFormValidation() {
  const forms = document.querySelectorAll('form[data-validate]');
  
  forms.forEach(form => {
    form.addEventListener('submit', function(e) {
      e.preventDefault();
      
      let isValid = true;
      const formData = new FormData(form);
      
      // Clear previous errors
      form.querySelectorAll('.error-message').forEach(err => err.remove());
      form.querySelectorAll('.error').forEach(field => field.classList.remove('error'));
      
      // Validate required fields
      form.querySelectorAll('[required]').forEach(field => {
        if (!field.value.trim()) {
          isValid = false;
          showError(field, 'This field is required');
        }
      });
      
      // Validate email fields
      form.querySelectorAll('input[type="email"]').forEach(field => {
        if (field.value && !isValidEmail(field.value)) {
          isValid = false;
          showError(field, 'Please enter a valid email address');
        }
      });
      
      if (isValid) {
        // Show success message
        showSuccessMessage(form);
        
        // Reset form after short delay
        setTimeout(() => {
          form.reset();
        }, 2000);
      }
    });
  });
}

function showError(field, message) {
  field.classList.add('error');
  
  const errorDiv = document.createElement('div');
  errorDiv.className = 'error-message text-red-500 text-sm mt-1';
  errorDiv.textContent = message;
  
  field.parentNode.appendChild(errorDiv);
}

function showSuccessMessage(form) {
  const successDiv = document.createElement('div');
  successDiv.className = 'success-message bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded mb-4';
  successDiv.innerHTML = '<strong>Success!</strong> Your message has been sent. We\'ll get back to you soon.';
  
  form.insertBefore(successDiv, form.firstChild);
  
  setTimeout(() => {
    successDiv.remove();
  }, 5000);
}

function isValidEmail(email) {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return re.test(email);
}

// Animate on Scroll
function initAnimateOnScroll() {
  const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
  };
  
  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('fade-in-up');
        observer.unobserve(entry.target);
      }
    });
  }, observerOptions);
  
  // Observe elements with data-animate attribute
  document.querySelectorAll('[data-animate]').forEach(el => {
    observer.observe(el);
  });
}

// Cookie Banner
function initCookieBanner() {
  const cookieBanner = document.getElementById('cookie-banner');
  const acceptCookiesBtn = document.getElementById('accept-cookies');
  
  if (cookieBanner && acceptCookiesBtn) {
    // Check if user has already accepted cookies
    const cookiesAccepted = localStorage.getItem('cookiesAccepted');
    
    if (!cookiesAccepted) {
      // Show banner after short delay
      setTimeout(() => {
        cookieBanner.classList.add('active');
      }, 1000);
    }
    
    acceptCookiesBtn.addEventListener('click', function() {
      localStorage.setItem('cookiesAccepted', 'true');
      cookieBanner.classList.remove('active');
    });
  }
}

// Utility Functions

// Debounce function for performance
function debounce(func, wait) {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      clearTimeout(timeout);
      func(...args);
    };
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
  };
}

// Get current year for footer
function setCurrentYear() {
  const yearElements = document.querySelectorAll('.current-year');
  const currentYear = new Date().getFullYear();
  
  yearElements.forEach(el => {
    el.textContent = currentYear;
  });
}

// Initialize year on load
setCurrentYear();
