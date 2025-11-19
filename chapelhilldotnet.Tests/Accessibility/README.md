# Accessibility Tests

This directory contains comprehensive accessibility tests for the Chapel Hill .NET Blazor application to ensure WCAG 2.1 Level AA compliance.

## Overview

These tests validate that the application is accessible to all users, including those using assistive technologies such as screen readers, keyboard-only navigation, and other adaptive devices.

## Test Categories

### Component-Level Tests

#### 1. **EventCardAccessibilityTests**
Tests the EventCard component for:
- Semantic HTML (`<article>`, `<time>` elements)
- Proper datetime attributes for dates
- ARIA hidden attributes on decorative icons
- Heading hierarchy

#### 2. **OrganizerCardAccessibilityTests**
Tests the OrganizerCard component for:
- Semantic HTML structure
- Image alt text
- Social media link ARIA labels
- External link security attributes (`target="_blank"`, `rel="noopener noreferrer"`)
- Icon accessibility

#### 3. **DarkModeToggleAccessibilityTests**
Tests the DarkModeToggle component for:
- Button type attribute
- ARIA labels and pressed states
- Live region announcements (aria-live)
- SVG icon accessibility (aria-hidden, focusable="false")
- Keyboard accessibility
- Focus visible styles

#### 4. **NavMenuAccessibilityTests**
Tests the NavMenu component for:
- Proper button implementation (not checkbox)
- ARIA expanded/controls attributes
- Navigation landmark labels
- Keyboard navigation
- State management
- Icon accessibility

#### 5. **MainLayoutAccessibilityTests**
Tests the MainLayout component for:
- Skip navigation link
- Main content landmarks
- Proper ARIA roles and labels
- Landmark structure
- Skip link positioning

### Integration Tests

#### 6. **GeneralAccessibilityTests**
Cross-cutting accessibility tests including:
- Landmark roles (banner, main, contentinfo)
- Navigation labels
- Section labeling with aria-labelledby
- Button type attributes
- Tabindex validation (no positive values)
- Table accessibility (caption, scope)
- Time element datetime attributes
- External link security
- Heading hierarchy
- Alert regions

## WCAG 2.1 AA Principles Tested

### ✅ Perceivable
- **Text Alternatives**: Alt text on images, ARIA labels on icon-only buttons
- **Time-Based Media**: Semantic time elements with machine-readable formats
- **Adaptable**: Semantic HTML structure, proper heading hierarchy
- **Distinguishable**: Focus indicators, contrast (manual testing required)

### ✅ Operable
- **Keyboard Accessible**: All interactive elements keyboard accessible
- **Enough Time**: N/A for static content
- **Navigable**: Skip links, landmarks, proper focus management
- **Input Modalities**: Button vs checkbox for navigation toggle

### ✅ Understandable
- **Readable**: Lang attribute, semantic structure
- **Predictable**: Consistent navigation, expected behavior
- **Input Assistance**: ARIA labels, error alerts

### ✅ Robust
- **Compatible**: Proper ARIA usage, semantic HTML, valid attributes

## Running the Tests

### All Accessibility Tests
```bash
dotnet test --filter "FullyQualifiedName~Accessibility"
```

### Specific Test Class
```bash
dotnet test --filter "FullyQualifiedName~EventCardAccessibilityTests"
```

### Single Test
```bash
dotnet test --filter "FullyQualifiedName~EventCard_UsesSemanticArticleElement"
```

### With Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Test Framework

- **bUnit**: Blazor component testing framework
- **xUnit**: Test runner
- **AngleSharp**: HTML parsing for assertions

## What These Tests Don't Cover

The following require manual testing or additional tools:

### Manual Testing Required
1. **Color Contrast**: WCAG requires 4.5:1 for normal text, 3:1 for large text
   - Use browser DevTools or [WebAIM Contrast Checker](https://webaim.org/resources/contrastchecker/)
2. **Screen Reader Testing**: Test with actual screen readers
   - NVDA (Windows - free)
   - JAWS (Windows - paid)
   - VoiceOver (macOS - built-in)
3. **Keyboard Navigation Flow**: Verify tab order makes sense
4. **Focus Visible**: Verify focus indicators are clearly visible
5. **Zoom to 200%**: Ensure layout doesn't break at 200% zoom

### Automated Accessibility Scanning
Consider adding these tools for comprehensive coverage:

1. **axe-core**: Automated accessibility testing
   ```bash
   npm install axe-playwright
   ```

2. **Lighthouse**: Chrome DevTools accessibility audit
   ```bash
   lighthouse https://your-site.com --only-categories=accessibility
   ```

3. **Pa11y**: Command-line accessibility testing
   ```bash
   npm install -g pa11y
   pa11y https://your-site.com
   ```

## Best Practices

### Adding New Components
When creating new components, add corresponding accessibility tests:

1. Semantic HTML elements
2. ARIA attributes (labels, roles, states)
3. Keyboard navigation
4. Focus management
5. Screen reader announcements (live regions)

### Test Naming Convention
```csharp
[Fact]
public void ComponentName_BehaviorUnderTest_ExpectedResult()
{
    // Arrange
    // Act
    // Assert
}
```

### Example Test
```csharp
[Fact]
public void Button_HasAriaLabel_WhenIconOnly()
{
    // Arrange
    var cut = RenderComponent<IconButton>();

    // Act
    var button = cut.Find("button");

    // Assert
    Assert.NotNull(button.GetAttribute("aria-label"));
}
```

## CI/CD Integration

Add to your build pipeline:

```yaml
- name: Run Accessibility Tests
  run: dotnet test --filter "FullyQualifiedName~Accessibility" --logger trx

- name: Publish Test Results
  uses: actions/upload-artifact@v2
  with:
    name: accessibility-test-results
    path: '**/*.trx'
```

## Resources

- [WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)
- [ARIA Authoring Practices](https://www.w3.org/WAI/ARIA/apg/)
- [WebAIM Resources](https://webaim.org/resources/)
- [bUnit Documentation](https://bunit.dev/)
- [Deque University](https://dequeuniversity.com/)

## Continuous Improvement

Accessibility is an ongoing commitment. Consider:

1. Regular manual testing with assistive technologies
2. User testing with people who use assistive technologies
3. Staying updated with WCAG guidelines
4. Adding new tests as issues are discovered
5. Training team members on accessibility best practices

## Reporting Issues

If you discover accessibility issues:

1. Add a failing test that demonstrates the issue
2. Document the WCAG criterion violated
3. Provide expected behavior
4. Submit a PR with the fix and passing test
