# Accessibility Testing Guide

This guide provides instructions for manual accessibility testing to complement the automated tests.

## Quick Start Checklist

- [ ] Keyboard Navigation (30 min)
- [ ] Screen Reader Testing (1 hour)
- [ ] Color Contrast Verification (15 min)
- [ ] Zoom and Responsive Testing (15 min)
- [ ] Automated Scanning (10 min)

## 1. Keyboard Navigation Testing

### Goal
Ensure all functionality is accessible without a mouse.

### Steps

#### Basic Navigation
1. **Tab Through Page**
   - Press `Tab` repeatedly through entire page
   - Verify: Focus indicator is always visible
   - Verify: Tab order is logical (left-to-right, top-to-bottom)
   - Verify: Skip link appears on first Tab

2. **Reverse Navigation**
   - Press `Shift+Tab` to go backwards
   - Verify: Works consistently

3. **Interactive Elements**
   - **Buttons**: Press `Enter` or `Space` to activate
   - **Links**: Press `Enter` to follow
   - **Mobile Menu**: Press `Enter` or `Space` to toggle
   - **Dark Mode Toggle**: Press `Enter` or `Space` to switch modes

4. **Skip Navigation**
   - Press `Tab` once from page load
   - Press `Enter` on "Skip to main content"
   - Verify: Focus moves to main content area

#### Test Results Template
```
✅ All interactive elements reachable via keyboard
✅ Focus indicators visible on all elements
✅ Tab order is logical
✅ Skip navigation works
✅ No keyboard traps (can always navigate away)
```

## 2. Screen Reader Testing

### Tools
- **Windows**: [NVDA](https://www.nvaccess.org/download/) (Free)
- **macOS**: VoiceOver (Built-in, press `Cmd+F5`)
- **Browser Extensions**: [Screen Reader for Chrome](https://chrome.google.com/webstore/detail/screen-reader)

### NVDA Quick Start (Windows)

#### Installation
1. Download NVDA from nvaccess.org
2. Install and restart
3. NVDA starts with `Ctrl+Alt+N`
4. Stop with `Insert+Q`

#### Basic Commands
- `Insert+Down Arrow`: Read next item
- `Insert+Up Arrow`: Read previous item
- `Insert+Space`: Toggle browse/focus mode
- `H`: Jump to next heading
- `B`: Jump to next button
- `K`: Jump to next link
- `T`: Jump to next table
- `L`: Jump to next list

### VoiceOver Quick Start (macOS)

#### Activation
- Press `Cmd+F5` to start/stop
- `Ctrl+Option` = VO key

#### Basic Commands
- `VO+Right Arrow`: Move to next item
- `VO+Left Arrow`: Move to previous item
- `VO+Cmd+H`: Next heading
- `VO+Space`: Activate element

### What to Test

#### Navigation
```
Test: Start NVDA/VoiceOver
Navigate to homepage
Listen for:
✅ "Skip to main content, link"
✅ "Banner, region"
✅ "Navigation, Primary navigation"
✅ Each nav link announced correctly
```

#### Landmarks
```
Test: Navigate by landmarks
Press Insert+F7 (NVDA) or VO+U (VoiceOver)
Verify landmarks:
✅ Banner
✅ Navigation
✅ Main
✅ Content Info (Footer)
```

#### Headings
```
Test: Navigate by headings (Press H)
Verify heading hierarchy:
✅ Level 1: "Connect, Learn, and Grow with .NET & Azure"
✅ Level 2: "What We're About"
✅ Level 3: ".NET Development", "Azure Cloud", etc.
✅ No skipped levels (no h1 → h3 without h2)
```

#### Forms & Buttons
```
Test: Navigate to buttons
Verify announcements:
✅ "Join Us, button"
✅ "Toggle navigation menu, button, collapsed" (or "expanded")
✅ "Switch to dark mode, button, not pressed" (or "pressed")
```

#### Tables (Events Page)
```
Test: Navigate to Past Events table
Verify:
✅ Table announced with caption
✅ Column headers announced
✅ Cell content associated with headers
```

#### Images
```
Test: Navigate to images
Verify:
✅ Decorative icons not announced (aria-hidden)
✅ Content images have alt text
✅ Logo images have descriptive text
```

#### Live Regions
```
Test: Toggle dark mode
Verify:
✅ "Dark mode enabled" or "Light mode enabled" announced
✅ Announcement is polite (doesn't interrupt)
```

### Screen Reader Test Results Template
```
Page: Home
Screen Reader: NVDA 2024.1
Date: [DATE]

Navigation:
✅ Skip link announced and works
✅ All landmarks present
✅ Heading hierarchy correct
✅ All buttons have labels

Forms:
✅ All inputs have labels
✅ Error messages announced

Images:
✅ Alt text appropriate
✅ Decorative images hidden

Overall: PASS / FAIL
Issues: [List any issues found]
```

## 3. Color Contrast Verification

### Tools
1. **WebAIM Contrast Checker**: https://webaim.org/resources/contrastchecker/
2. **Chrome DevTools**: Built-in contrast ratio tool
3. **Colour Contrast Analyser**: Desktop app

### Using Chrome DevTools

1. **Open DevTools** (`F12`)
2. **Select Element** (Use Element Picker)
3. **Check Contrast**:
   - Look for contrast ratio in Styles panel
   - Ratio should show ✓ or ✗ next to AA/AAA

### Requirements

| Element | Minimum Ratio | Level |
|---------|--------------|--------|
| Normal text (<18pt) | 4.5:1 | AA |
| Large text (≥18pt or ≥14pt bold) | 3:1 | AA |
| UI components & graphics | 3:1 | AA |

### Test These Elements

```
✅ Body text on white background
✅ Body text on gray background
✅ Links on white background
✅ Button text on blue background
✅ Navigation text on dark background
✅ Footer text on dark background
✅ Form labels
✅ Error messages
✅ Focus indicators
```

### Contrast Test Results Template
```
Element: Primary Button
Foreground: #FFFFFF (white)
Background: #1b6ec2 (blue)
Ratio: 4.6:1
Result: ✅ PASS (meets AA for normal text)

Element: Navigation Link
Foreground: #d7d7d7 (light gray)
Background: rgba(0,0,0,0.4)
Ratio: 8.2:1
Result: ✅ PASS (exceeds AA requirements)
```

## 4. Zoom and Responsive Testing

### Test at Different Zoom Levels

1. **100% Zoom (Baseline)**
   - Verify everything displays correctly

2. **200% Zoom (WCAG Requirement)**
   - Press `Ctrl +` (or `Cmd +` on Mac) repeatedly
   - Verify:
     - ✅ All content visible
     - ✅ No horizontal scrolling required
     - ✅ Text doesn't overlap
     - ✅ Functionality still works

3. **400% Zoom**
   - Continue zooming
   - Verify: Page still usable (nice to have, not required)

### Test at Different Viewport Sizes

```
Desktop: 1920x1080
  ✅ Navigation displayed
  ✅ Three-column layout
  ✅ Skip link visible on focus

Tablet: 768x1024
  ✅ Mobile menu appears
  ✅ Two-column layout
  ✅ All content accessible

Mobile: 375x667
  ✅ Mobile menu works
  ✅ Single-column layout
  ✅ Touch targets ≥44x44px
  ✅ No horizontal scroll
```

## 5. Automated Scanning

### Lighthouse (Chrome DevTools)

1. **Open DevTools** (`F12`)
2. **Go to Lighthouse Tab**
3. **Select "Accessibility"**
4. **Click "Generate Report"**
5. **Target**: Score ≥ 95

### axe DevTools Extension

1. **Install**: [axe DevTools](https://www.deque.com/axe/devtools/)
2. **Open DevTools** (`F12`)
3. **Go to axe DevTools Tab**
4. **Click "Scan ALL of my page"**
5. **Fix all Critical and Serious issues**

### Pa11y (Command Line)

```bash
# Install
npm install -g pa11y

# Run
pa11y http://localhost:5000

# With specific standard
pa11y --standard WCAG2AA http://localhost:5000

# Generate HTML report
pa11y --reporter html http://localhost:5000 > report.html
```

## 6. Common Issues to Look For

### Critical Issues
- [ ] Images without alt text
- [ ] Form inputs without labels
- [ ] Buttons without accessible names
- [ ] Missing page title
- [ ] Insufficient color contrast
- [ ] Keyboard traps

### Important Issues
- [ ] Skip navigation not working
- [ ] Missing landmark roles
- [ ] Improper heading hierarchy
- [ ] Missing ARIA labels on icon-only buttons
- [ ] Tables without headers or caption
- [ ] No visible focus indicators

### Minor Issues
- [ ] Missing language attribute
- [ ] Empty links or buttons
- [ ] Redundant alt text ("image of...")
- [ ] Missing aria-current on nav
- [ ] Unlabeled sections

## 7. Testing Schedule

### On Every PR
- [ ] Run automated unit tests
- [ ] Quick keyboard navigation check
- [ ] Lighthouse scan

### Weekly
- [ ] Full screen reader test (one page)
- [ ] Color contrast spot check
- [ ] Zoom testing

### Before Release
- [ ] Complete manual testing checklist
- [ ] Full screen reader testing (all pages)
- [ ] Complete color contrast audit
- [ ] Third-party accessibility audit (recommended)

## 8. Reporting Issues

### Issue Template

```markdown
## Accessibility Issue

**WCAG Criterion**: [e.g., 2.1.1 Keyboard]
**Level**: [A, AA, or AAA]
**Severity**: [Critical, High, Medium, Low]

**Location**: [Page/Component]
**Element**: [Specific element with issue]

**Issue Description**:
[What's wrong]

**Expected Behavior**:
[What should happen]

**Steps to Reproduce**:
1. [Step 1]
2. [Step 2]

**Screenshot/Video**:
[If applicable]

**Assistive Technology Used**:
[e.g., NVDA 2024.1, Keyboard only, etc.]

**Suggested Fix**:
[If you have a solution]
```

## 9. Resources

### Learning
- [WebAIM Articles](https://webaim.org/articles/)
- [A11y Project Checklist](https://www.a11yproject.com/checklist/)
- [MDN Accessibility Guide](https://developer.mozilla.org/en-US/docs/Web/Accessibility)

### Testing Tools
- [WAVE Browser Extension](https://wave.webaim.org/extension/)
- [Accessibility Insights](https://accessibilityinsights.io/)
- [Stark Plugin](https://www.getstark.co/) (Figma/Design)

### Screen Reader Guides
- [NVDA User Guide](https://www.nvaccess.org/files/nvda/documentation/userGuide.html)
- [VoiceOver User Guide](https://support.apple.com/guide/voiceover/welcome/mac)
- [JAWS Keyboard Shortcuts](https://www.freedomscientific.com/training/jaws/hotkeys/)

### Community
- [WebAIM Discussion List](https://webaim.org/discussion/)
- [A11y Slack](https://web-a11y.slack.com/)
- [Stack Overflow [accessibility]](https://stackoverflow.com/questions/tagged/accessibility)

## 10. Certification

Once all tests pass:
- [ ] All automated tests pass
- [ ] Manual keyboard testing complete
- [ ] Screen reader testing complete
- [ ] Color contrast verified
- [ ] Zoom/responsive testing done
- [ ] Lighthouse score ≥ 95
- [ ] No critical axe issues
- [ ] Documentation updated

**Accessibility Status**: ✅ WCAG 2.1 AA Compliant

---

**Last Updated**: [DATE]
**Tested By**: [NAME]
**Next Review**: [DATE + 3 months]
