export type LoginActionState = {
  success: boolean;
  errors?: string[];
};

export const defaultLoginActionState: LoginActionState = {
  success: false,
};
