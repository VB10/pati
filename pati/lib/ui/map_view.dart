import 'dart:async';

import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class MapView extends StatefulWidget {
  @override
  _MapViewState createState() => _MapViewState();
}

class _MapViewState extends State<MapView> {
  Completer<GoogleMapController> _controller = Completer();

  BitmapDescriptor _markerIcon;

  LatLng _kMapCenter = LatLng(40.875257, 29.1);
  @override
  Widget build(BuildContext context) {
    _createMarkerImageFromAsset(context);

    return Scaffold(
      body: GoogleMap(
        markers: _createMarker(),
        mapType: MapType.normal,
        initialCameraPosition: CameraPosition(target: _kMapCenter, zoom: 10),
        onMapCreated: (GoogleMapController controller) {
          _controller.complete(controller);
        },
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: null,
        label: Text('To the lake!'),
        icon: Icon(Icons.directions_boat),
      ),
    );
  }

  Set<Marker> _createMarker() {
    // TODO(iskakaushik): Remove this when collection literals makes it to stable.
    // https://github.com/flutter/flutter/issues/28312
    // ignore: prefer_collection_literals
    return <Marker>[
      Marker(
        markerId: MarkerId("marker_1"),
        infoWindow: InfoWindow(
          title: "Adalar Mama Kabi",
          snippet: "Doluluk orani %15"
        ),
        position: _kMapCenter,
        icon: _markerIcon,
        onTap: (){

        },

      ),
    ].toSet();
  }

  Future<void> _createMarkerImageFromAsset(BuildContext context) async {
    if (_markerIcon == null) {
      final ImageConfiguration imageConfiguration =
          createLocalImageConfiguration(context);
      BitmapDescriptor.fromAssetImage(
              imageConfiguration, 'assets/images/container.png')
          .then(_updateBitmap);
    }
  }

  void _updateBitmap(BitmapDescriptor bitmap) {
    setState(() {
      _markerIcon = bitmap;
    });
  }
}
