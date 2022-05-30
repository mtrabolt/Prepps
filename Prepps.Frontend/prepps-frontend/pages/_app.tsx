import '../styles/globals.css'
import type { AppProps } from 'next/app'

function MyApp({ Component, pageProps }: AppProps) {

  if ("development" == process.env.ENVIRONMENT) {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
  }

  return <Component {...pageProps} />
}

export default MyApp
