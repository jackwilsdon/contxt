# Versioning
Assembly and Assembly File Versions should be the same.

## Format

### `A.B.C.D`

 Symbol | Name            | Description
-------:|:----------------|:------------
 **A**  | Major Number    | Incremented if any backwards incompatible changes are made to the public API.
 **B**  | Minor Number    | Incremented if any backwards compatible changes are made to the public API. An example of this would be adding functionality in a backwards compatible way.
 **C**  | Build Number    | Incremented for every build (version change).
 **D**  | Revision Number | Incremented if any backwards compatible bug fixes are made.

See **[semver.org](http://semver.org)** for more information.
