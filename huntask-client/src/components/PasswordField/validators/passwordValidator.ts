export function validatePassword(password: string) {
  const errors = [];

  if (password.length < 6) {
    errors.push('Password must be at least 6 characters long.');
  }
  if (!/[a-z]/.test(password)) {
    errors.push('Password must contain a lowercase letter.');
  }
  if (!/[A-Z]/.test(password)) {
    errors.push('Password must contain an uppercase letter.');
  }
  if (!/\d/.test(password)) {
    errors.push('Password must contain a digit.');
  }
  if (!/[^a-zA-Z0-9]/.test(password)) {
    errors.push('Password must contain a symbol.');
  }

  return errors.length === 0 ? { isValid: true } : { isValid: false, error: errors[0] }; // return first error only (MUI-style)
}
