// Copy only design tokens from the pinned @neetechs/ui package into ASP.NET static assets.
import {readFileSync, writeFileSync, mkdirSync} from 'node:fs';
import {dirname, resolve, relative, sep} from 'node:path';
import {fileURLToPath} from 'node:url';
import {argv} from 'node:process';
const entry = fileURLToPath(import.meta.resolve('@neetechs/ui/tokens.css'));
const root = dirname(entry);
const css = readFileSync(entry, 'utf8').replace(/^\uFEFF/, '');
const parts = [...css.matchAll(/@import ['"]\.\/([^'"]+)['"];/g)].map((match) => match[1]);
if (parts.length < 5 || parts.length !== (css.match(/@import /g) || []).length) {
  throw new Error('Unexpected Neetechs UI tokens.css import list');
}
const content = '/* Generated from @neetechs/ui@0.2.0 tokens.css; run npm run ui:sync to refresh. */\n'
  + parts.map((part) => {
      const location = resolve(root, part);
      if (relative(root, location).startsWith('..' + sep) || dirname(location) !== root) {
        throw new Error('Unsafe nested token import ' + part);
      }
      return '/* --- ' + part + ' --- */\n' + readFileSync(location, 'utf8').replace(/^\uFEFF/, '').trimEnd();
    }).join('\n\n') + '\n';
const target = resolve('Neetechs_MVC/wwwroot/vendor/neetechs-ui/tokens.css');
if (argv.includes('--check')) {
  if (readFileSync(target, 'utf8') !== content) throw new Error('Committed MVC token CSS differs from @neetechs/ui. Run npm run ui:sync.');
  console.log('MVC CSS matches installed @neetechs/ui.');
} else {
  mkdirSync(dirname(target), {recursive: true});
  writeFileSync(target, content);
  console.log('Synced MVC Neetechs UI tokens.');
}
