# SchoolConnect Marketing Website

This directory contains the marketing website for SchoolConnect - a comprehensive education platform for schools, teachers, parents, and students.

## Overview

The marketing website is built with:
- **HTML5** with semantic markup
- **Tailwind CSS** (via CDN) for styling
- **Vanilla JavaScript** for interactivity
- **Font Awesome** for icons
- **Mobile-first responsive design**

## Directory Structure

```
src/website/
├── index.html                    # Homepage
├── features.html                 # Comprehensive features page
├── pricing.html                  # Pricing/subscription plans
├── about.html                    # About SchoolConnect  
├── contact.html                  # Contact form page
├── for-schools.html              # Features for school administrators
├── for-teachers.html             # Features for teachers
├── for-parents.html              # Features for parents
├── for-students.html             # Features for students
├── css/
│   └── styles.css                # Custom styles (extends Tailwind)
├── js/
│   └── main.js                   # JavaScript for interactivity
├── images/
│   └── .gitkeep                  # Placeholder for images
└── README.md                     # This file
```

## Pages

### Homepage (index.html)
The main landing page featuring:
- Hero section with CTAs
- Trusted by section with stats
- Key features overview
- Stakeholder sections
- Testimonials
- Pricing preview
- CTA banner
- Footer

### Features (features.html)
Comprehensive list of all platform features organized by module:
- Institution Management
- Academic Management
- Lesson Delivery
- Assessment & Grading
- Communication
- Collaboration
- Calendar & Scheduling
- Enrolment & Admissions

### Pricing (pricing.html)
Pricing plans and comparison:
- Free Plan (up to 50 students)
- Basic Plan ($99/month)
- Standard Plan ($249/month) - Most Popular
- Premium Plan ($499/month)
- Enterprise Plan (Custom pricing)
- Feature comparison table
- FAQ section

### Stakeholder Pages
- **For Schools**: Benefits for administrators, principals, and school owners
- **For Teachers**: Tools and features for educators
- **For Parents**: Stay connected with child's education
- **For Students**: Organize school life and access learning materials

### About (about.html)
Company information:
- Mission and vision
- Company story
- Team section
- Values and culture
- Partners and integrations
- Careers

### Contact (contact.html)
Contact information and form:
- Contact form (name, email, school, message)
- Email: hello@schoolconnect.co.za
- Support information
- Social media links

## Features

### JavaScript Functionality
- Mobile navigation toggle
- Sticky header on scroll
- FAQ accordion
- Smooth scrolling for anchor links
- Form validation
- Animation on scroll
- Cookie banner

### Design Elements
- Primary color: Blue (#2563EB)
- Secondary color: Green (#10B981)
- Accent color: Orange (#F59E0B)
- Modern, clean typography
- Rounded corners and shadows
- Smooth hover transitions
- Responsive breakpoints: Mobile (<640px), Tablet (640-1024px), Desktop (>1024px)

### Accessibility
- ARIA labels
- Keyboard navigation support
- Color contrast compliance
- Focus states on interactive elements
- Skip navigation link
- Semantic HTML5 elements

## Setup and Development

### Local Development
1. Clone the repository
2. Navigate to `src/website/`
3. Open `index.html` in a web browser
4. No build process required - uses CDN for Tailwind CSS

### Deployment

The website is static HTML and can be deployed to any web hosting service:

#### Option 1: GitHub Pages
1. Push to GitHub repository
2. Enable GitHub Pages in repository settings
3. Select the branch and `/src/website` folder

#### Option 2: Netlify
1. Connect repository to Netlify
2. Set build directory to `src/website`
3. No build command needed
4. Deploy

#### Option 3: Vercel
1. Import repository to Vercel
2. Set root directory to `src/website`
3. Deploy

#### Option 4: Traditional Web Host
1. Upload all files in `src/website/` to your web server
2. Ensure `index.html` is set as the default document
3. Configure HTTPS

### CDN Resources Used
- Tailwind CSS: https://cdn.tailwindcss.com
- Font Awesome: https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css

## Call-to-Action URLs

The website uses the following CTAs pointing to the SchoolConnect portal:

- **Sign Up**: `https://portal.schoolconnect.co.za/signup`
- **Login**: `https://portal.schoolconnect.co.za/login`
- **Free Trial**: `https://portal.schoolconnect.co.za/signup?trial=true`
- **Request Demo**: `https://portal.schoolconnect.co.za/demo`

## Browser Support

The website supports:
- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile browsers (iOS Safari, Chrome Mobile)

## Performance

- Minimal external dependencies (only CDN resources)
- Optimized images (placeholders for now)
- Fast loading times
- Mobile-optimized

## SEO

Each page includes:
- Proper heading hierarchy (H1, H2, H3)
- Meta descriptions
- Open Graph tags
- Semantic HTML5 elements
- Alt text placeholders for images

## Customization

### Colors
Edit the CSS variables in `css/styles.css`:
```css
:root {
  --primary-color: #2563EB;
  --secondary-color: #10B981;
  --accent-color: #F59E0B;
  --text-dark: #1F2937;
  --text-light: #6B7280;
  --bg-light: #F9FAFB;
}
```

### Content
All content can be edited directly in the HTML files. Each page is self-contained with its own content.

### Images
Replace image placeholders in the `images/` directory with actual images. Update the `src` attributes in HTML files accordingly.

## Support

For questions or issues:
- Email: hello@schoolconnect.co.za
- Visit: [SchoolConnect Website](https://schoolconnect.co.za)

## License

Copyright © 2024 SchoolConnect. All rights reserved.
