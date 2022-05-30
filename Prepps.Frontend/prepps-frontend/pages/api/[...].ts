import type {NextApiRequest, NextApiResponse} from 'next'
import {IncomingHttpHeaders} from "http2";

export class PassthroughRequest {
    method: string = '';
    body?: string = undefined;
    headers: IncomingHttpHeaders = {};
}

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse<any>
) {
    const { url, method, body, headers } = req
    const apiRoute = 'https://localhost:5001';
    
    let forwardUrl = apiRoute + url?.split(/\/*api/g).splice(1).join('/') || '';
    
    let passthroughData: PassthroughRequest = {
        method: method || '',
        headers: headers,
        body: body === '' ? undefined : JSON.stringify(body),
    }
    
    let result = await passthrough(forwardUrl, passthroughData);
    
    res.end(await result.text());
}

const passthrough = async (url = '', metaData: PassthroughRequest): Promise<Response> => {
    let headersJson = JSON.stringify(metaData.headers);
    let headers = JSON.parse(headersJson);
    
    return await fetch(url, {
        method: metaData.method || '',
        body: metaData.body,
        mode: 'cors',
        headers: new Headers(headers),
    });
}
