import { CacheProvider } from '@emotion/react';
import createCache from '@emotion/cache';
import { prefixer } from 'stylis';
import rtlPlugin from 'stylis-plugin-rtl';
// this component is for making material ui right to left(farsi)
// i copy this
// Create rtl cache
const cacheRtl = createCache({
    key: 'muirtl',
    stylisPlugins: [prefixer, rtlPlugin],
});

export function MaterialUiRTLProvider(props : any) {
    return (
       <CacheProvider value={cacheRtl}>
             {props.children}
       </CacheProvider>
    )
}