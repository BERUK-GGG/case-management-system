// Input validation and sanitization utilities

export const validateInput = {
  // Email validation
  email: (email) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  },

  // Phone number validation (Swedish format)
  phone: (phone) => {
    const phoneRegex = /^(\+46|0)[0-9]{8,9}$/;
    return phoneRegex.test(phone.replace(/\s/g, ''));
  },

  // Name validation (only letters, spaces, hyphens)
  name: (name) => {
    const nameRegex = /^[a-zA-ZåäöÅÄÖ\s\-']+$/;
    return nameRegex.test(name) && name.length >= 2 && name.length <= 50;
  },

  // Registration number validation (Swedish car registration)
  regNumber: (regNum) => {
    const regRegex = /^[A-Z]{3}[0-9]{2}[A-Z0-9]{1}$/;
    return regRegex.test(regNum.replace(/\s/g, '').toUpperCase());
  },

  // VIN number validation
  vin: (vin) => {
    const vinRegex = /^[A-HJ-NPR-Z0-9]{17}$/;
    return vinRegex.test(vin.replace(/\s/g, '').toUpperCase());
  },

  // Required field validation
  required: (value) => {
    return value !== null && value !== undefined && value.toString().trim().length > 0;
  },

  // Length validation
  length: (value, min = 0, max = 255) => {
    const length = value ? value.toString().length : 0;
    return length >= min && length <= max;
  }
};

export const sanitizeInput = {
  // Remove HTML tags and dangerous characters
  text: (input) => {
    if (!input) return '';
    return input
      .toString()
      .replace(/<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi, '') // Remove script tags
      .replace(/<[^>]*>/g, '') // Remove HTML tags
      .replace(/[<>&"']/g, (match) => { // Escape dangerous characters
        const map = {
          '<': '&lt;',
          '>': '&gt;',
          '&': '&amp;',
          '"': '&quot;',
          "'": '&#x27;'
        };
        return map[match];
      })
      .trim();
  },

  // Sanitize phone number
  phone: (phone) => {
    if (!phone) return '';
    return phone.toString().replace(/[^\d+\-\s]/g, '').trim();
  },

  // Sanitize registration number
  regNumber: (regNum) => {
    if (!regNum) return '';
    return regNum.toString().replace(/[^A-Za-z0-9]/g, '').toUpperCase();
  },

  // Sanitize VIN
  vin: (vin) => {
    if (!vin) return '';
    return vin.toString().replace(/[^A-Za-z0-9]/g, '').toUpperCase();
  },

  // Sanitize email
  email: (email) => {
    if (!email) return '';
    return email.toString().toLowerCase().trim();
  }
};

// Form validation rules for react-hook-form
export const validationRules = {
  name: {
    required: 'Namn krävs',
    validate: (value) => validateInput.name(value) || 'Ogiltigt namn (endast bokstäver, mellanslag och bindestreck)',
    minLength: { value: 2, message: 'Namn måste vara minst 2 tecken' },
    maxLength: { value: 50, message: 'Namn får inte vara längre än 50 tecken' }
  },

  email: {
    required: 'E-post krävs',
    validate: (value) => validateInput.email(value) || 'Ogiltig e-postadress'
  },

  phone: {
    required: 'Telefonnummer krävs',
    validate: (value) => validateInput.phone(value) || 'Ogiltigt telefonnummer (använd svenskt format)'
  },

  regNumber: {
    required: 'Registreringsnummer krävs',
    validate: (value) => validateInput.regNumber(value) || 'Ogiltigt registreringsnummer (använd format ABC123)'
  },

  vin: {
    validate: (value) => !value || validateInput.vin(value) || 'Ogiltigt VIN-nummer (17 tecken)'
  },

  required: {
    required: 'Detta fält krävs'
  },

  description: {
    maxLength: { value: 1000, message: 'Beskrivning får inte vara längre än 1000 tecken' }
  }
};
