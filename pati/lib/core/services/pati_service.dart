import 'package:http/http.dart' as http;

class PatiService {
  static PatiService _instance = PatiService._init();
  PatiService._init();
  static PatiService instance = _instance;

  String _baseUrl = "https://127.0.0.1:5001";

  Future<dynamic> getPatiService() async {
    final response = await http.get("$_baseUrl/pet");  
    return response.body;
  }
}
