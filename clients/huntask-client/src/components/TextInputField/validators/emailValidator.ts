export const validateEmail = (value: string) => {
  // TODO[validation]: use zod
  const isValid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
  return isValid
    ? { isValid: true }
    : { isValid: false, error: 'Please enter a valid email address' };
};
