import { NextRequest, NextResponse } from 'next/server';

function isPublicPath(pathname: string): boolean {
  const publicPaths = process.env.PUBLIC_PATHS?.split(',') || [];
  return publicPaths.some((publicPath) => pathname.startsWith(publicPath));
}

export function authorizationMiddleware(req: NextRequest): NextResponse | unknown {
  const { pathname } = req.nextUrl;

  if (isPublicPath(pathname)) {
    return NextResponse.next();
  }

  const authTokenCookieName = process.env.AUTH_TOKEN_COOKIE_NAME || 'AuthToken';
  const token = req.cookies.get(authTokenCookieName)?.value;

  if (!token) {
    const loginPath = process.env.LOGIN_PATH || '/login';
    const loginUrl = new URL(loginPath, req.url);

    // TODO[auth]: Handle redirect after login on the login page
    loginUrl.searchParams.set('redirect', pathname);

    // TODO[auth]: Add jwt validation by issuer and audience

    return NextResponse.redirect(loginUrl, 302);
  }

  return NextResponse.next();
}
