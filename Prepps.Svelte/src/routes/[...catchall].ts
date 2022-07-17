/** @type {import('@sveltejs/kit').RequestHandler} */
export async function get({request}) {

  let result = await handler(request)

    return {
      status: 200,
      headers: {
        'access-control-allow-origin': '*'
      },
      body: {
        result: result
      }
    };
  }

/** @type {import('@sveltejs/kit').RequestHandler} */
export async function post() {
  return {
    status: 200,
    headers: {
      'access-control-allow-origin': '*'
    },
    body: {
      number: Math.random()
    }
  };
}

/** @type {import('@sveltejs/kit').RequestHandler} */
export async function put() {
  return {
    status: 200,
    headers: {
      'access-control-allow-origin': '*'
    },
    body: {
      number: Math.random()
    }
  };
}

async function handler(req) {
  const { url, method, body, headers } = req
  const apiRoute = import.meta.env.VITE_API_ENDPOINT
  
  let forwardUrl = apiRoute + url?.split(/\/*api/g).splice(1).join('/') || '';

  let passthroughData: PassthroughRequest = {
      method: method || '',
      headers: headers,
      body: body === null ? undefined : JSON.stringify(body),
  }

  let result = await passthrough(forwardUrl, passthroughData);

  let resultString = await result.text();
  return resultString;
}

const passthrough = async (url = '', metaData: PassthroughRequest): Promise<Response> => {
  let headersJson = JSON.stringify(metaData.headers);
  let headers = JSON.parse(headersJson);

  return await fetch(url, {
      method: metaData.method || '',
      body: metaData.body ?? undefined,
      mode: 'cors',
      headers: new Headers(headers),
  });
}

export class PassthroughRequest {
  method: string = '';
  body?: string = undefined;
  headers: Headers;
}