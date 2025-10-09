// Security utilities

/**
 * Generate a CSRF token for form submissions
 * @returns {string} CSRF token
 */
export const generateCSRFToken = () => {
  const array = new Uint8Array(32);
  crypto.getRandomValues(array);
  return Array.from(array, byte => byte.toString(16).padStart(2, '0')).join('');
};

/**
 * Store CSRF token in sessionStorage
 * @param {string} token - CSRF token to store
 */
export const setCSRFToken = (token) => {
  sessionStorage.setItem('csrfToken', token);
};

/**
 * Get CSRF token from sessionStorage
 * @returns {string|null} CSRF token or null if not found
 */
export const getCSRFToken = () => {
  return sessionStorage.getItem('csrfToken');
};

/**
 * Content Security Policy violations handler
 * @param {SecurityPolicyViolationEvent} event - CSP violation event
 */
export const handleCSPViolation = (event) => {
  console.warn('CSP Violation:', {
    blockedURI: event.blockedURI,
    violatedDirective: event.violatedDirective,
    originalPolicy: event.originalPolicy,
    documentURI: event.documentURI,
    referrer: event.referrer,
    lineNumber: event.lineNumber,
    columnNumber: event.columnNumber,
    sourceFile: event.sourceFile
  });
  
  // In production, you might want to report this to your security monitoring service
  if (process.env.NODE_ENV === 'production') {
    // Report to security monitoring service
    // Example: securityService.reportCSPViolation(event);
  }
};

/**
 * Rate limiting utility for form submissions
 */
export class RateLimiter {
  constructor(maxRequests = 5, windowMs = 60000) { // 5 requests per minute by default
    this.maxRequests = maxRequests;
    this.windowMs = windowMs;
    this.requests = new Map();
  }

  /**
   * Check if request is allowed
   * @param {string} key - Identifier for the request (e.g., user ID, IP address)
   * @returns {boolean} Whether the request is allowed
   */
  isAllowed(key) {
    const now = Date.now();
    const windowStart = now - this.windowMs;
    
    if (!this.requests.has(key)) {
      this.requests.set(key, []);
    }
    
    const userRequests = this.requests.get(key);
    
    // Remove old requests outside the current window
    const validRequests = userRequests.filter(timestamp => timestamp > windowStart);
    this.requests.set(key, validRequests);
    
    // Check if under the limit
    if (validRequests.length < this.maxRequests) {
      validRequests.push(now);
      return true;
    }
    
    return false;
  }

  /**
   * Get remaining requests for a key
   * @param {string} key - Identifier for the request
   * @returns {number} Number of remaining requests
   */
  getRemainingRequests(key) {
    const now = Date.now();
    const windowStart = now - this.windowMs;
    
    if (!this.requests.has(key)) {
      return this.maxRequests;
    }
    
    const userRequests = this.requests.get(key);
    const validRequests = userRequests.filter(timestamp => timestamp > windowStart);
    
    return Math.max(0, this.maxRequests - validRequests.length);
  }
}

/**
 * Secure session storage utilities
 */
export const secureStorage = {
  /**
   * Set item in sessionStorage with encryption (basic obfuscation)
   * @param {string} key - Storage key
   * @param {any} value - Value to store
   */
  setItem: (key, value) => {
    try {
      const jsonValue = JSON.stringify(value);
      const encodedValue = btoa(jsonValue); // Basic encoding
      sessionStorage.setItem(key, encodedValue);
    } catch (error) {
      console.error('Error storing secure item:', error);
    }
  },

  /**
   * Get item from sessionStorage with decryption
   * @param {string} key - Storage key
   * @returns {any} Decoded value or null
   */
  getItem: (key) => {
    try {
      const encodedValue = sessionStorage.getItem(key);
      if (!encodedValue) return null;
      
      const jsonValue = atob(encodedValue);
      return JSON.parse(jsonValue);
    } catch (error) {
      console.error('Error retrieving secure item:', error);
      return null;
    }
  },

  /**
   * Remove item from sessionStorage
   * @param {string} key - Storage key
   */
  removeItem: (key) => {
    sessionStorage.removeItem(key);
  },

  /**
   * Clear all items from sessionStorage
   */
  clear: () => {
    sessionStorage.clear();
  }
};

/**
 * Initialize security features
 */
export const initializeSecurity = () => {
  // Generate and store CSRF token
  const csrfToken = generateCSRFToken();
  setCSRFToken(csrfToken);
  
  // Add CSP violation event listener
  document.addEventListener('securitypolicyviolation', handleCSPViolation);
  
  // Set up session timeout warning
  let sessionTimeout;
  let warningTimeout;
  
  const resetSessionTimer = () => {
    clearTimeout(sessionTimeout);
    clearTimeout(warningTimeout);
    
    // Warn user 5 minutes before session expires
    warningTimeout = setTimeout(() => {
      const shouldExtend = window.confirm(
        'Din session kommer att löpa ut om 5 minuter. Vill du förlänga sessionen?'
      );
      
      if (shouldExtend) {
        // Extend session by making a keep-alive request
        fetch('/api/keep-alive', {
          method: 'POST',
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('authToken')}`,
            'X-CSRF-Token': getCSRFToken()
          }
        }).catch(() => {
          // If keep-alive fails, logout
          window.location.href = '/login';
        });
      }
    }, 19 * 60 * 1000); // 19 minutes (5 minutes before 24 hour expiry)
    
    // Auto logout after 24 hours
    sessionTimeout = setTimeout(() => {
      alert('Din session har löpt ut. Du kommer att loggas ut.');
      localStorage.clear();
      sessionStorage.clear();
      window.location.href = '/login';
    }, 24 * 60 * 60 * 1000); // 24 hours
  };
  
  // Reset timer on user activity
  const events = ['mousedown', 'mousemove', 'keypress', 'scroll', 'touchstart'];
  events.forEach(event => {
    document.addEventListener(event, resetSessionTimer, true);
  });
  
  // Initial timer setup
  resetSessionTimer();
};

// Rate limiter instance for general use
export const defaultRateLimiter = new RateLimiter();
